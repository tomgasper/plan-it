/* eslint-disable @typescript-eslint/no-unsafe-call */
/* eslint-disable @typescript-eslint/no-unsafe-member-access */
import React, { useState, useCallback, useMemo, useEffect } from 'react';
import { CancelDrop, DndContext, DragOverlay, Modifiers } from '@dnd-kit/core';
import { SortableContext, SortingStrategy } from '@dnd-kit/sortable';
import { createPortal } from 'react-dom';
import { Button, Flex, Group, useMantineTheme } from '@mantine/core';

import { useMultipleContainers } from '../../hooks/useMultipleProjectsHook';
import { DroppableContainer } from './Container/DroppableContainer';
import { SortableItem } from './Item/SortableItem';
import classes from './MultipleSortableProjects.module.css';

import { IconCirclePlus } from '@tabler/icons-react';
import { Project, ProjectTask } from '../../types/Project';
import { useCreateProjectTaskMutation, useDeleteProjectMutation, useMoveTaskToAnotherProjectMutation } from '../../services/planit-api';
import { notifications } from '@mantine/notifications';
import { useDisclosure } from '@mantine/hooks';
import { ExtendedModal } from '../Common/ExtendedModal';
import { NewTaskModal } from '../Task/NewTaskModal';

const PLACEHOLDER_ID = 'placeholder';

export interface MultipleSortableProjectsProps {
  adjustScale?: boolean;
  cancelDrop?: CancelDrop;
  onAddNewProject: any;
  refetchWorkspaceProjects: () => void;
  columns?: number;
  containerStyle?: React.CSSProperties;
  handle?: boolean;
  strategy?: SortingStrategy;
  modifiers?: Modifiers;
  scrollable?: boolean;
  workspaceProjects: Items;
}

export function MultipleSortableProjects({
  adjustScale = false,
  cancelDrop,
  onAddNewProject,
  refetchWorkspaceProjects,
  columns,
  handle = false,
  workspaceProjects,
  containerStyle,
  modifiers,
  strategy,
  scrollable,
}: MultipleSortableProjectsProps) {
  const theme = useMantineTheme();
  const [projects, setProjects] = useState(workspaceProjects);
  const [deleteProject] = useDeleteProjectMutation();
  const [createProjectTask] = useCreateProjectTaskMutation();
  const [ moveTaskToAnotherProject ] = useMoveTaskToAnotherProjectMutation();
  const [modalOpened, { open, close }] = useDisclosure(false);
  const [projectIdToAddNewTask, setProjectIdToAddNewTask] = useState<string | null>(null);

  useEffect(() => {
    setProjects(workspaceProjects);
  },[workspaceProjects]);

  useEffect(() => {
    setProjects(structuredClone(workspaceProjects));
  }, [JSON.stringify(workspaceProjects)])

  const handleMoveTaskToNewContainer = async (oldProjectId : string, taskId: string, newProjectId: string) => {
    const result = await moveTaskToAnotherProject({ projectId: oldProjectId, taskId, newProjectId });

    if (result.error)
      {
        console.error('Error moving task to another project', result.error);
    
        notifications.show({
          title: 'Error moving task to antoher project',
          message: 'Could not move task to another project, please try again!',
          color: 'red'
        });

        refetchWorkspaceProjects();
    
        return;
      }

    notifications.show({
      title: 'Success',
      message: 'Task moved successfully',
      color: 'green'
  });}

  const containers = useMemo(() => Object.keys(projects), [projects]);

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
  } = useMultipleContainers(handleMoveTaskToNewContainer, projects, setProjects);
  
  const handleAddTask = (projectId: string, addedTask: ProjectTask) => {
    setProjects((prevProjects: Projects) => {
      if (!prevProjects[projectId]) {
        console.error(`Project with id: ${projectId} doesn't exist`);
        return prevProjects;
      }
  
      if (prevProjects[projectId].projectTasks.some(task => task.id === addedTask.id)) {
        console.log(`Task with id: ${addedTask.id} alread exists in the project with id: ${projectId}`);
        return prevProjects;
      }
  
      return {
        ...prevProjects,
        [projectId]: {
          ...prevProjects[projectId],
          projectTasks: [...prevProjects[projectId].projectTasks, addedTask]
        }
      };
    });
  };
  

  const handleAddColumn = async () => {
    const result = await onAddNewProject();
  
    if (result.error)
    {
      console.error('Error adding new project :', result.error);
  
      notifications.show({
        title: 'Error adding new project',
        message: 'Could not add new project, please try again!',
        color: 'red'
      });
  
      return;
    }
  
    const newProject: Project = result.data;
  
    setProjects((prevProjects : object) => ({
      ...prevProjects,
      [newProject.id]: {
        workspaceId: newProject.workspaceId,
        id: newProject.id,
        name: newProject.name,
        description: newProject.description,
        projectTasks: []
      },
    }))};

  const handleUpdate = (updatedProject : Project) => {
    setProjects((prevProjects : Project) => ({
      ...prevProjects,
      [updatedProject.id]: {...updatedProject}
    }));
  }

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

  const onDeleteTask = (projectId, taskId) => {
    setProjects((prevProjects) => {
      const newProjects = {...prevProjects};
      newProjects[projectId].projectTasks = newProjects[projectId].projectTasks.filter((task) => task.id !== taskId);

      newProjects[projectId] = {...newProjects[projectId]};

      return newProjects;
    });
  }

  const onUpdateTask = (projectId, taskId, updatedTask) => {
    setProjects((prevProjects) => {
      const newProjects = {...prevProjects};
      newProjects[projectId].projectTasks = newProjects[projectId].projectTasks.map((task) => {
        if (task.id === taskId) {
          return updatedTask;
        }
        return task;
      });

      return newProjects;
    })};

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
        <ExtendedModal title="New project" opened={modalOpened} onClose={close}>
            <NewTaskModal closeWindow={close} projectId={projectIdToAddNewTask} onClose={ (task: ProjectTask) => {
                      handleAddTask(projectIdToAddNewTask, task)
                      setProjectIdToAddNewTask(null); }} />
        </ExtendedModal>
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
              onUpdate = {(updatedProject) => handleUpdate(updatedProject) }
              onRemove={() => handleRemove(containerId)}
            >
              <SortableContext items={projects[containerId].projectTasks.map((task) => task.id)} strategy={strategy}>
                {projects[containerId].projectTasks.map((task) => (
                  <SortableItem
                    onUpdateTask={onUpdateTask}
                    onDeleteTask={() => onDeleteTask(projects[containerId].id, task.id) }
                    index={task.id} 
                    key={task.id}
                    id={task.id}
                    projectId={projects[containerId].id}
                    content={task}
                    handle={handle}
                    containerId={containerId}
                    getIndex={getIndex} />
                ))}
                <Group p={15} justify='center'>
                  <IconCirclePlus
                    onClick={() => {
                      setProjectIdToAddNewTask(projects[containerId].id);
                      open();
                    }}
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
