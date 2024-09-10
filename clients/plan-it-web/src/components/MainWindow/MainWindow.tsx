import { Flex, Group, Title } from "@mantine/core";
import { MultipleSortableProjects } from '../SortableItems/MultipleSortableProjects';
import classes from './MainWindow.module.css';
import { useCreateProjectMutation, useGetProjectsForWorkspaceQuery } from "../../services/planit-api";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { WorkspaceMenu } from "./WorkspaceMenu";

export function MainWindow() {
    const { workspaceId } = useParams<{ workspaceId: string }>();
    const { data : projects, error, isLoading, refetch } = useGetProjectsForWorkspaceQuery(workspaceId, {
        skip: !workspaceId
    });
    const [ createProject, {isLoading: isUpdating }] = useCreateProjectMutation();

    useEffect(() => {
        if (workspaceId) {
          refetch().catch(console.error);
        }
      }, [workspaceId, refetch]);

      useEffect(() => {
        console.log('Refetched projects:', projects);
      }, [projects]);

    if (isLoading) {
        return <div>Loading projects...</div>;
      }
    
      if (error) {
        return <div>Error loading projects: {error.toString()}</div>;
      }

    const handleAddNewProject = async () => {
        const newProject = await createProject({
            workspaceId: workspaceId,
            name: "New Project",
            description: "New Project Description",
            projectTasks: []
        });

        refetch().catch(console.error);

        return newProject.data;
    }

    const projectsToObjects = projects?.projects.reduce((prev, cur) => ({...prev, [cur.id]: cur}), {});

    return (
        <Flex className={classes.container} direction="column">
            {isLoading ? (
            <div>Loading...</div>
            ) : (
            <>
                <Group>
                  <Group gap={15}>
                  <Title>Workspace</Title>
                    <WorkspaceMenu />
                  </Group>
                 
                  </Group>
                {projects?.projects && projects.projects.length > 0 ? (
                <MultipleSortableProjects
                    projectsIn={projectsToObjects}
                    onAddNewProject={handleAddNewProject}
                />
                ) : (
                    <>
                    <div>No projects available</div>
                    <button onClick={ handleAddNewProject }>Add new project</button>
                    </>
                )}
            </>
            )}
        </Flex>
    );  
}