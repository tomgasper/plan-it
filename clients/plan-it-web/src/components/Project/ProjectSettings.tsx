import {  Button, Flex, Group, Loader, Stack, TextInput, Title } from "@mantine/core";
import classes from "./projectSettings.module.css"
import {  useNavigate } from "react-router-dom";
import { useState } from "react";
import { notifications } from '@mantine/notifications';
import { useGetProjectQuery, useUpdateProjectMutation } from "../../services/planit-api";
import { Project } from "../../types/Project";

interface ProjectSettingsProps {
    onUpdate: (project: Project) => void;
    onRemove: () => void;
    projectId: string;
}

export function ProjectSettings({
    onUpdate,
    onRemove,
    projectId} : ProjectSettingsProps)
{
    const navigate = useNavigate();
    // Get the project ID from the URL

    if (!projectId)
    {
        console.error('No project ID found');

        notifications.show({
            title: 'Erorr accessing project',
            message: 'project was not found, please try again!',
            color: 'red'
          })

        navigate('/');

        return;
    }

     // Get details about the project and project projects
    const { data: project, error : projectError , isLoading : projectLoading } = useGetProjectQuery(projectId);

    // Local state for the project settings
    const [ projectName, setProjectName ] = useState(project?.name);
    const [ projectDescription, setProjectDescription ] =useState(project?.description);

    const [ updateproject, { isLoading : projectUpdating } ] = useUpdateProjectMutation();
    
    
    const handleSaveproject = async () => {
        // Save the project settings
        const updatedProject = {
            name: projectName,
            description: projectDescription
        };

        const result = await updateproject({updatedProject, projectId: projectId});

        if (result.error)
        {
            console.error('Error updating project:', result.error);
            notifications.show({
                title: 'Erorr updating project',
                message: `${result.error.data.title}`,
                color: 'red'
              })
            return;
        }

        onUpdate(result.data);

        notifications.show({
            title: 'Success',
            message: 'Project settings saved successfuly',
            color: 'green'
          });
    }

    return (
        <Flex className={classes.container} justify="center" align="center">
            {(projectLoading) &&  <Loader />}
                <Stack mb={25}>
                    <Stack>
                        <TextInput label="Name" placeholder={project?.name} required value={projectName} onChange={ e => setProjectName(e.currentTarget.value) } />
                        <TextInput label="Description" placeholder={project?.description} required value={projectDescription} onChange={ e => setProjectDescription(e.currentTarget.value) }/>
                    </Stack>
                    <Group justify="space-between">
                    <Button variant='light' color='red' onClick={onRemove} >Delete</Button>
                    <Button  color="blue" onClick={ handleSaveproject } loading={projectUpdating }>Save</Button>
                    </Group>
                
                </Stack>
        </Flex>
    )
}