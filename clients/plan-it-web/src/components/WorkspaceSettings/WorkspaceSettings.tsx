import {  Button, Flex, Paper, Stack, Text, TextInput, Title } from "@mantine/core";
import classes from "./WorkspaceSettings.module.css"
import {  useNavigate, useParams } from "react-router-dom";
import { useGetProjectsForWorkspaceQuery, useGetWorkspaceQuery, useUpdateWorkspaceMutation } from "../../services/planit-api";
import { WorkspaceProjectsTable } from "./WorkspaceProjectsTable";
import { useState } from "react";
import { updateWorkspaceLocal } from "../../redux/workspacesSlice";
import { useAppDispatch } from "../../hooks/reduxHooks";
import { notifications } from '@mantine/notifications';

export function WorkspaceSettings()
{
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    // Get the workspace ID from the URL
    const { workspaceId } = useParams<{ workspaceId: string }>();

     // Get details about the workspace and workspace projects
    const { data: workspace, error : workspaceError , isLoading : workspaceLoading } = useGetWorkspaceQuery(workspaceId ?? '');
    const { data : projects, error : projectsError , isLoading : projectsLoading, refetch } = useGetProjectsForWorkspaceQuery(workspaceId ?? '');

    if (workspaceError || projectsError)
    {
        console.error('Error loading workspace settings:', workspaceError, projectsError);
        notifications.show({
            title: 'Error loading workspace settings',
            message: 'Could not load workspace settings, please try again!',
            color: 'red'
          })
        navigate('/');
    }

    // Local state for the workspace settings
    const [ workspaceName, setWorkspaceName ] = useState(workspace?.name);
    const [ workspaceDescription, setWorkspaceDescription ] =useState(workspace?.description);

    const [ updateWorkspace, { isLoading : workspaceUpdating } ] = useUpdateWorkspaceMutation();
    
    
    const handleSaveWorkspace = async () => {
        // Save the workspace settings
        const updatedWorkspace = {
            name: workspaceName,
            description: workspaceDescription
        };

        const result = await updateWorkspace({updatedWorkspace, workspaceId: workspaceId});

        if (result.error)
        {
            console.error('Error updating workspace:', result.error);
            notifications.show({
                title: 'Erorr updating workspace',
                message: 'Could not update workspace, please try again!',
                color: 'red'
              })
            return;
        }

        dispatch(updateWorkspaceLocal(result.data));
    }

    return (
        <Flex className={classes.container} justify="center" align="center">
            {(workspaceLoading || projectsLoading) && <Text>Loading...</Text>}
            <Paper withBorder shadow="md" p={30} mt={30} radius="md">
                <Stack>
                    <Stack>
                        <Title order={2}>Workspace Settings</Title>
                        <TextInput label="Name" placeholder={workspace?.name} required value={workspaceName} onChange={ e => setWorkspaceName(e.currentTarget.value) } />
                        <TextInput label="Description" placeholder={workspace?.description} required value={workspaceDescription} onChange={ e => setWorkspaceDescription(e.currentTarget.value) }/>
                    </Stack>
                <Title mt={20} order={6}>Projects</Title>
                <WorkspaceProjectsTable projects={projects ? projects?.projects : []} />
                <Button variant="light" color="blue" onClick={ handleSaveWorkspace } loading={workspaceUpdating }>Save</Button>
                </Stack>
            </Paper>
        </Flex>
    )
}