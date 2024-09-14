import { Flex, Group, Loader, Title } from "@mantine/core";
import { MultipleSortableProjects } from '../SortableItems/MultipleSortableProjects';
import classes from './MainWindow.module.css';
import { useCreateProjectMutation, useGetProjectsForWorkspaceQuery, useGetWorkspaceQuery } from "../../services/planit-api";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect } from "react";
import { WorkspaceMenu } from "./WorkspaceMenu";
import { useAppSelector } from "../../hooks/reduxHooks";

export function MainWindow() {
    const navigate = useNavigate();
    const { workspaces }= useAppSelector(state => state.workspaces);
    const { workspaceId } = useParams<{ workspaceId: string }>();

    const { data: workspace, error: workspaceFetchError, isLoading: isLoadingWorkspace, refetch : refetchWorkspace } = useGetWorkspaceQuery(workspaceId ?? "");
    const { data : projects, error: workspaceProjectsFetchError, isLoading, refetch } = useGetProjectsForWorkspaceQuery(workspaceId ?? "", {
        skip: !workspaceId
    });
    const [ createProject ] = useCreateProjectMutation();

    console.log(projects);

    useEffect(() => {
        if (workspaceId) {
          refetchWorkspace().catch(console.error);
          refetch().catch(console.error);
        }

        if (!workspaceId && workspaces && workspaces.length > 0) {
          navigate(`/workspaces/${workspaces[0].id}`);
        }
      }, [workspace, navigate, workspaces, workspaceId, refetch, refetchWorkspace]);

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
                  <Title>{workspace!.name} </Title>
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