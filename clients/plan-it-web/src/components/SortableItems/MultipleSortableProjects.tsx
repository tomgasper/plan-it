import React, { useState, useCallback, useMemo, useEffect } from 'react';
import { CancelDrop, DndContext, DragOverlay, Modifiers } from '@dnd-kit/core';
import { SortableContext, SortingStrategy } from '@dnd-kit/sortable';
import { createPortal } from 'react-dom';
import { Box, Button, Flex, Group, useMantineTheme } from '@mantine/core';

import { useMultipleContainers } from '../../hooks/useMultipleProjectsHook';
import { DroppableContainer } from './Container/DroppableContainer';
import { SortableItem } from './Item/SortableItem';
import { getNextContainerId } from '../../utils/containerUtils';
import classes from './MultipleSortableProjects.module.css';

import { IconCirclePlus } from '@tabler/icons-react';
import { Project } from '../../types/Project';
import { useDeleteProjectMutation } from '../../services/planit-api';

const PLACEHOLDER_ID = 'placeholder';

export interface MultipleSortableProjectsProps {
  adjustScale?: boolean;
  cancelDrop?: CancelDrop;
  onAddNewProject: () => Promise<Project>;
  columns?: number;
  containerStyle?: React.CSSProperties;
  handle?: boolean;
  strategy?: SortingStrategy;
  modifiers?: Modifiers;
  scrollable?: boolean;
  projectsIn: Record<string, Project>;
}

export function MultipleSortableProjects({
  adjustScale = false,
  cancelDrop,
  onAddNewProject,
  columns,
  handle = false,
  projectsIn,
  containerStyle,
  modifiers,
  strategy,
  scrollable,
}: MultipleSortableProjectsProps) {
  const theme = useMantineTheme();
  const [projects, setProjects] = useState(projectsIn);
  const [deleteProject] = useDeleteProjectMutation();

  console.log(projects);

  useEffect(() => {
    setProjects(structuredClone(projectsIn));
  }, [JSON.stringify(projectsIn)])

  const {
    sensors,
    activeId,
    onDragStart,
    onDragOver,
    onDragEnd,
    onDragCancel,
    getIndex,
    renderSortableItemDragOverlay,
    renderContainerDragOverlay,
  } = useMultipleContainers(projects, setProjects);
  
  const containers = useMemo(() => Object.keys(projects), [projects]);

  const handleAddColumn = async () => {
    const newProject = await onAddNewProject();
    const newContainerId = getNextContainerId();
    setProjects((prevProjects) => ({
      ...prevProjects,
      [newContainerId]: {
        workspaceId: newProject.workspaceId,
        id: newProject.id,
        name: newProject.name,
        description: newProject.description,
        projectTasks: []
      },
    }))};

  const handleRemove = useCallback(async (containerID: string) => {
    const result = await deleteProject(projects[containerID].id);
    if (result.error) {
      console.error(result.error);
      return;
    }
    setProjects((prevProjects) => {
      const newProjects = {...prevProjects};
      delete newProjects[containerID];
      return newProjects;
    });
  }, [deleteProject, projects]);

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
                    style={{
                      width: "45px",
                      height: "45px",
                      color: theme.colors.blue[7],
                      cursor: "pointer"
                    }}
                    stroke={1.3} />
                </Group>
              </SortableContext>
            </DroppableContainer>
          ))}
        </SortableContext>
        <Flex p={15} align='center' justify='center'>
        <Button justify='center'
            id={PLACEHOLDER_ID}
            onClick={handleAddColumn}
            placeholder
          >
            + Add column
          </Button>
          </Flex>
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
