import { Flex, Group, Title } from "@mantine/core";
import { MultipleSortableProjects } from '../SortableItems/MultipleSortableProjects';
import classes from './MainWindow.module.css';
import { useCreateProjectMutation, useGetProjectsForWorkspaceQuery, useGetWorkspaceQuery } from "../../services/planit-api";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { WorkspaceMenu } from "./WorkspaceMenu";
import { Project } from "../../types/Project";

export function MainWindow() {
    const { workspaceId } = useParams<{ workspaceId: string }>();

    const { data: workspace, error: workspaceFetchError, isLoading: isLoadingWorkspace } = useGetWorkspaceQuery(workspaceId ?? "");
    const { data : projects, error: workspaceProjectsFetchError, isLoading, refetch } = useGetProjectsForWorkspaceQuery(workspaceId ?? "", {
        skip: !workspaceId
    });
    const [ createProject ] = useCreateProjectMutation();

    useEffect(() => {
        if (workspaceId) {
          refetch().catch(console.error);
        }
      }, [workspaceId, refetch]);

      useEffect(() => {
        console.log('Refetched projects:', projects);
      }, [projects]);
    
      if (workspaceFetchError) {
        return <div>Incorrect workspace</div>;
      }

      if (workspaceProjectsFetchError)
      {
        return <div>Could not retrieve projects for this workspace</div>;
      }

    const handleAddNewProject = async () => {
        const newProject = await createProject({
            workspaceId: workspaceId,
            name: "New Project",
            description: "New Project Description",
            projectTasks: []
        });
    
        refetch().catch(console.error);
    
        return newProject;
    }

    const projectsToObjects = projects?.projects.reduce((prev, cur) => ({...prev, [cur.id]: cur}), {});

    return (
        <Flex className={classes.container} direction="column">
            {isLoading || isLoadingWorkspace ? (
            <div>Loading...</div>
            ) : (
            <>
                <Group>
                  <Group gap={15}>
                  <Title>{workspace!.name}</Title>
                    <WorkspaceMenu />
                  </Group>
                 
                  </Group>
                {projects?.projects && projects.projects.length > 0 ? (
                <MultipleSortableProjects
                    workspaceProjects={projectsToObjects}
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