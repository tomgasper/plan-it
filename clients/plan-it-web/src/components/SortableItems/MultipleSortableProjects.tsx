import React, { useState } from 'react';
import { CancelDrop, DndContext, DragOverlay, Modifiers } from '@dnd-kit/core';
import { SortableContext, SortingStrategy } from '@dnd-kit/sortable';
import { createPortal } from 'react-dom';
import { Flex, Group, useMantineTheme } from '@mantine/core';

import { projects, useMultipleContainers } from '../../hooks/useMultipleProjectsHook';
import { DroppableContainer } from './Container/DroppableContainer';
import { SortableItem } from './Item/SortableItem';
import { getNextContainerId } from '../../utils/containerUtils';
import classes from './MultipleSortableProjects.module.css';

import { IconCirclePlus } from '@tabler/icons-react';

const PLACEHOLDER_ID = 'placeholder';

export interface MultipleSortableProjectsProps {
  adjustScale?: boolean;
  cancelDrop?: CancelDrop;
  columns?: number;
  containerStyle?: React.CSSProperties;
  projects?: projects;
  handle?: boolean;
  strategy?: SortingStrategy;
  modifiers?: Modifiers;
  scrollable?: boolean;
}


export function MultipleSortableProjects({
  adjustScale = false,
  cancelDrop,
  columns,
  handle = false,
  projects: initialprojects,
  containerStyle,
  modifiers,
  strategy,
  scrollable,
}: MultipleSortableProjectsProps) {
  const theme = useMantineTheme();
  const [projects, setprojects] = useState<projects>(initialprojects);

  const {
    sensors,
    activeId,
    containers,
    setContainers,
    onDragStart,
    onDragOver,
    onDragEnd,
    onDragCancel,
    getIndex,
    renderSortableItemDragOverlay,
    renderContainerDragOverlay,
  } = useMultipleContainers(projects, setprojects);

  const handleRemove = (containerID: string) => {
    setContainers((containers) => containers.filter((id) => id !== containerID));
  };

  const handleAddColumn = () => {
    const newContainerId = getNextContainerId();
    setContainers((containers) => [...containers, newContainerId]);
    setprojects((projects) => ({
      ...projects,
      [newContainerId]: {
        name: "New project",
        description: "",
        projectTasks: []
      },
    }));
  };

  return (
    <DndContext
      sensors={sensors}
      onDragStart={onDragStart}
      onDragOver={onDragOver}
      onDragEnd={onDragEnd}
      onDragCancel={onDragCancel}
      cancelDrop={cancelDrop}
      modifiers={modifiers}
    >
      <Flex className={classes.projectsContainer}>
        <SortableContext items={[...containers, PLACEHOLDER_ID]} strategy={strategy}>
          {containers.map((containerId) => (
            <DroppableContainer
              key={containerId}
              id={containerId}
              label={`Column ${containerId}`}
              content={projects[containerId]}
              columns={columns}
              items={projects[containerId].projectTasks.map((task) => task.id)}
              scrollable={scrollable}
              style={containerStyle}
              onRemove={() => handleRemove(containerId)}
            >
              <SortableContext items={projects[containerId].projectTasks.map((task) => task.id)} strategy={strategy}>
                {projects[containerId].projectTasks.map((task) => (
                  <SortableItem
                    index={task.id} 
                    key={task.id}
                    id={task.id}
                    content={task}
                    handle={handle}
                    containerId={containerId}
                    getIndex={getIndex} />
                ))}
                <Group p={15} justify='center'>
                  <IconCirclePlus
                  onClick={() => console.log("Add task")}
                  style={{ width: "45px",
                    height: "45px",
                    color:theme.colors.blue[7],
                    cursor: "pointer"
                   }}
                  stroke={1.3} />
                  </Group>
              </SortableContext>
            </DroppableContainer>
          ))}
          <DroppableContainer
            id={PLACEHOLDER_ID}
            items={[]}
            onClick={handleAddColumn}
            placeholder
          >
            + Add column
          </DroppableContainer>
        </SortableContext>
      </Flex>
      {createPortal(
        <DragOverlay adjustScale={adjustScale}>
          {activeId
            ? containers.includes(activeId)
              ? renderContainerDragOverlay(activeId)
              : renderSortableItemDragOverlay(activeId)
            : null}
        </DragOverlay>,
        document.body
      )}
    </DndContext>
  );
}