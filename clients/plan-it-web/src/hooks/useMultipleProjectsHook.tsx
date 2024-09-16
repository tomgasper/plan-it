/* eslint-disable @typescript-eslint/no-unsafe-function-type */
import { useState, useRef, useCallback } from 'react';
import { useSensors, useSensor, MouseSensor, TouchSensor, UniqueIdentifier } from '@dnd-kit/core';
import { arrayMove } from '@dnd-kit/sortable';
import { getNextContainerId } from '../utils/containerUtils';
import { collisionDetectionStrategy } from '../utils/sortabelUtils';
import { Project } from '../types/Project';
import { Item } from '../components/SortableItems/Item';
import { Container } from '../components/SortableItems/Container/Container';

const PLACEHOLDER_ID = 'placeholder';

export type Items = Record<UniqueIdentifier, Project>;

export const useMultipleContainers = ( onMoveTaskToNewContainer : Function, items: Items, setItems: React.Dispatch<React.SetStateAction<Items>>) => {
  const [activeId, setActiveId] = useState<string | null>(null);
  const [activeContainerId, setActiveContainerId] = useState<string | null>(null);
  const [clonedItems, setClonedItems] = useState<Items | null>(null);
  const lastOverId = useRef<string | null>(null);
  const recentlyMovedToNewContainer = useRef(false);
  const sensors = useSensors(useSensor(MouseSensor), useSensor(TouchSensor));

  const findContainer = useCallback((id: string) => {
    if (id in items) {
      return id;
    }
    return Object.keys(items).find((key) => items[key].projectTasks.some((task) => task.id === id));
  }, [items]);

  const getIndex = useCallback((id: string) => {
    const container = findContainer(id);
    if (!container) {
      return -1;
    }
    return items[container].projectTasks.findIndex((task) => task.id === id);
  }, [items, findContainer]);

  const onDragStart = useCallback(({ active }) => {
    console.log(active.id);
    setActiveId(active.id);
    setActiveContainerId(findContainer(active.id));
    setClonedItems(items);
  }, [items]);

  const onDragOver = useCallback(({ active, over }) => {
    const overId = over?.id;
    if (!overId || active.id in items) return;

    const overContainer = findContainer(overId);
    const activeContainer = findContainer(active.id);

    if (!overContainer || !activeContainer) return;

    if (activeContainer !== overContainer) {
      setItems((prevItems) => {
        const activeItems = prevItems[activeContainer].projectTasks;
        const overItems = prevItems[overContainer].projectTasks;
        const overIndex = overId in prevItems ? overItems.length + 1 : overItems.findIndex((task) => task.id === overId);
        const activeIndex = activeItems.findIndex((task) => task.id === active.id);

        let newIndex: number;

        if (overId in prevItems) {
          newIndex = overItems.length + 1;
        } else {
          const isBelowOverItem = over && active.rect.current.translated &&
            active.rect.current.translated.top > over.rect.top + over.rect.height;

          newIndex = isBelowOverItem ? overIndex + 1 : overIndex;
        }

        recentlyMovedToNewContainer.current = true;

        return {
          ...prevItems,
          [activeContainer]: {
            ...prevItems[activeContainer],
            projectTasks: prevItems[activeContainer].projectTasks.filter((item) => item.id !== active.id)
          },
          [overContainer]: {
            ...prevItems[overContainer],
            projectTasks: [
              ...prevItems[overContainer].projectTasks.slice(0, newIndex),
              prevItems[activeContainer].projectTasks.find((item) => item.id === active.id)!,
              ...prevItems[overContainer].projectTasks.slice(newIndex)
            ],
          }
        };
      });
    }
  }, [items, findContainer, setItems]);

  const onDragEnd = useCallback(({ active, over }) => {
    if (active.id in items && over?.id) {
      setItems((prevItems) => {
        const activeIndex = Object.keys(prevItems).indexOf(active.id);
        const overIndex = Object.keys(prevItems).indexOf(over.id);
        return Object.fromEntries(
          arrayMove(Object.entries(prevItems), activeIndex, overIndex)
        );
      });
    }

    const activeContainer = findContainer(active.id);

    if (!activeContainer) {
      setActiveId(null);
      return;
    }

    const overId = over?.id;

    if (overId == null) {
      setActiveId(null);
      return;
    }

    if (overId === PLACEHOLDER_ID) {
      const newContainerId = getNextContainerId();
      setItems((prevItems) => ({
        ...prevItems,
        [activeContainer]: {
          ...prevItems[activeContainer],
          projectTasks: prevItems[activeContainer].projectTasks.filter((task) => task.id !== active.id)
        },
        [newContainerId]: {
          name: "New project",
          description: "",
          projectTasks: [prevItems[activeContainer].projectTasks.find((task) => task.id === active.id)!]
        },
      }));
      setActiveId(null);
      return;
    }

    const overContainer = findContainer(overId);

    if (overContainer) {
      const activeIndex = getIndex(active.id);
      const overIndex = getIndex(overId);

      if (overContainer != activeContainerId && !(active.id in items)) {
        console.log("Moved a task to a new container");

        const prevContainerId = activeContainerId;
        const currContainerId = overContainer;
        const itemId = activeId;

        onMoveTaskToNewContainer(prevContainerId, itemId, currContainerId);
      }

      if (activeIndex !== overIndex) {
        setItems((prevItems) => ({
          ...prevItems,
          [overContainer]: {
            ...prevItems[overContainer],
            projectTasks: arrayMove(prevItems[overContainer].projectTasks, activeIndex, overIndex)
          },
        }));
      }}

    setActiveId(null);
  }, [items, findContainer, getIndex, setItems]);

  const onDragCancel = useCallback(() => {
    if (clonedItems) {
      setItems(clonedItems);
    }
    setActiveId(null);
    setClonedItems(null);
  }, [clonedItems, setItems]);

  const renderSortableItemDragOverlay = useCallback((id: UniqueIdentifier) => {
    const containerId = findContainer(id) as UniqueIdentifier;
    const projectTask = items[containerId].projectTasks.find((task) => task.id === id);

    return (
      <Item
        value={projectTask.id}
        content={projectTask}
        handle={true}
        style={undefined}
        dragOverlay
      />
    );
  }, [items, findContainer]);

  const renderContainerDragOverlay = useCallback((containerId: UniqueIdentifier) => {
    return (
      <Container
        label={`Column ${containerId}`}
        content={items[containerId]}
        style={{
          height: '100%',
        }}
        shadow
        unstyled={false}
      >
        {items[containerId].projectTasks.map((item) => (
          <Item
            key={item.id}
            value={item.id}
            handle={true}
            content={item}
            style={undefined}
          />
        ))}
      </Container>
    );
  }, [items]);

  return {
    sensors,
    activeId,
    onDragStart,
    onDragOver,
    onDragEnd,
    onDragCancel,
    findContainer,
    getIndex,
    collisionDetectionStrategy: collisionDetectionStrategy(lastOverId, recentlyMovedToNewContainer, activeId, items),
    renderSortableItemDragOverlay,
    renderContainerDragOverlay,
  }}