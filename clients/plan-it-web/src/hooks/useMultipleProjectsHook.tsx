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

export const useMultipleContainers = (items: Items, setItems: React.Dispatch<React.SetStateAction<Items>>) => {
  const [containers, setContainers] = useState(Object.keys(items));
  const [activeId, setActiveId] = useState<string | null>(null);
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
  }, [findContainer, items]);

  const onDragStart = useCallback(({ active }) => {
    setActiveId(active.id);
    setClonedItems(items);
  }, [items]);

  const onDragOver = useCallback(({ active, over }) => {
    const overId = over?.id;
    if (!overId || active.id in items) return;

    const overContainer = findContainer(overId);
    const activeContainer = findContainer(active.id);

    if (!overContainer || !activeContainer) return;

    if (activeContainer !== overContainer) {
      setItems((items) => {
        const activeItems = items[activeContainer].projectTasks;
        const overItems = items[overContainer].projectTasks;
        const overIndex = overId in items ? overItems.length + 1 : overItems.findIndex((task) => task.id === overId);
        const activeIndex = activeItems.findIndex((task) => task.id === active.id);

        let newIndex: number;

        if (overId in items) {
          newIndex = overItems.length + 1;
        } else {
          const isBelowOverItem = over && active.rect.current.translated &&
            active.rect.current.translated.top > over.rect.top + over.rect.height;

          newIndex = isBelowOverItem ? overIndex + 1 : overIndex;
        }

        recentlyMovedToNewContainer.current = true;

        return {
          ...items,
          [activeContainer]: {
            ...items[activeContainer],
            projectTasks: items[activeContainer].projectTasks.filter((item) => item.id !== active.id)
          },
          [overContainer]: {
            ...items[overContainer],
            projectTasks: [
              ...items[overContainer].projectTasks.slice(0, newIndex),
              items[activeContainer].projectTasks.find((item) => item.id === active.id)!,
              ...items[overContainer].projectTasks.slice(newIndex)
            ],
          }
        };
      });
    }
  }, [findContainer, items]);

  const onDragEnd = useCallback(({ active, over }) => {
    if (active.id in items && over?.id) {
      setContainers((containers) => {
        const activeIndex = containers.indexOf(active.id);
        const overIndex = containers.indexOf(over.id);
        return arrayMove(containers, activeIndex, overIndex);
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
      setContainers((containers) => [...containers, newContainerId]);
      setItems((items) => ({
        ...items,
        [activeContainer]: {
          ...items[activeContainer],
          projectTasks: items[activeContainer].projectTasks.filter((task) => task.id !== active.id)
        },
        [newContainerId]: {
          name: "New project",
          description: "",
          projectTasks: [items[activeContainer].projectTasks.find((task) => task.id === active.id)!]
        },
      }));
      setActiveId(null);
      return;
    }

    const overContainer = findContainer(overId);

    if (overContainer) {
      const activeIndex = getIndex(active.id);
      const overIndex = getIndex(overId);

      if (activeIndex !== overIndex) {
        setItems((items) => ({
          ...items,
          [overContainer]: {
            ...items[overContainer],
            projectTasks: arrayMove(items[overContainer].projectTasks, activeIndex, overIndex)
          },
        }));
      }
    }

    setActiveId(null);
  }, [findContainer, getIndex, items]);

  const onDragCancel = useCallback(() => {
    if (clonedItems) {
      setItems(clonedItems);
    }
    setActiveId(null);
    setClonedItems(null);
  }, [clonedItems]);

  function renderSortableItemDragOverlay(id: UniqueIdentifier) {
    const containerId = findContainer(id) as UniqueIdentifier;
    const projectTask = items[containerId].projectTasks.find( (task) => task.id === id);

    return (
      <Item
        value={projectTask.id}
        content={projectTask}
        handle={true}
        style={undefined}
        dragOverlay
      />
    );
  }

  function renderContainerDragOverlay(containerId: UniqueIdentifier) {
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
        {items[containerId].projectTasks.map((item, index) => (
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
  }

  return {
    sensors,
    activeId,
    containers,
    setContainers,
    onDragStart,
    onDragOver,
    onDragEnd,
    onDragCancel,
    findContainer,
    getIndex,
    collisionDetectionStrategy: collisionDetectionStrategy(lastOverId, recentlyMovedToNewContainer, activeId, items),
    renderSortableItemDragOverlay,
    renderContainerDragOverlay,
  };
};
