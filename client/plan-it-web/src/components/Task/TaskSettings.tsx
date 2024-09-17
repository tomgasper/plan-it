import {  Button, Flex, Group, Loader, Stack, TextInput,Text, Avatar } from "@mantine/core";
import classes from "./TaskSettings.module.css"
import { useEffect, useState } from "react";
import { notifications } from '@mantine/notifications';
import { useGetProjectQuery, useUpdateProjectTaskMutation, } from "../../services/planit-api";
import { Task } from "../../types/Task";
import { ProjectTask } from "../../types/Project";
import { User } from "../../types/User";

interface TaskSettingsProps {
    onUpdate: (projectId: string, taskId: string, updatedTask: Task) => void;
    projectId: string;
    taskId: string;
    assignedUsers: User[];
}

export function TaskSettings({
    onUpdate,
    projectId,
    taskId,
    assignedUsers
} : TaskSettingsProps)
{
     // Get details about the Task and Task Tasks
    const { data: project, error : taskError , isLoading : projectLoading } = useGetProjectQuery(projectId);

    // Local state for the Task settings
    const [ taskName, setTaskName ] = useState("");
    const [ taskDescription, setTaskDescription ] =useState("");
    const [ updateProjectTask, { isLoading : TaskUpdating } ] = useUpdateProjectTaskMutation();

    let task : ProjectTask | undefined = undefined;
    if (!projectLoading)
    {
        if (project)
        {
            task = project.projectTasks.find(t => t.id === taskId);
        }
    }

    useEffect(() => {
        if (task)
        {
            setTaskName(task.name);
            setTaskDescription(task.description);
        }
    }, [task]);
    
    
    const handleSaveTask = async () => {
        // Save the Task settings
        const updatedTask = {
            name: taskName,
            description: taskDescription
        };

        const result = await updateProjectTask({projectId, taskId, updatedTask});

        if (result.error)
        {
            console.error('Error updating Task:', result.error);
            notifications.show({
                title: 'Erorr updating Task',
                message: `${result.error.data.title}`,
                color: 'red'
              })
            return;
        }

        onUpdate(projectId, taskId, result.data);

        notifications.show({
            title: 'Success',
            message: 'Task settings saved successfuly',
            color: 'green'
          });
    }

    return (
        <Flex className={classes.container} justify="center" align="center">
            { projectLoading &&  <Loader />}
                <Stack mb={25}>
                    <Stack>
                        <TextInput label="Name" placeholder={task?.name} required value={taskName} onChange={ e => setTaskName(e.currentTarget.value) } />
                        <TextInput label="Description" placeholder={task?.description} required value={taskDescription} onChange={ e => setTaskDescription(e.currentTarget.value) }/>
                    </Stack>
                    <Stack>
                        <Text>Assigned users:</Text>
                        <Group gap={5}>
                        {assignedUsers.map((user : User) => (
                            <Avatar alt={`${user.firstName} ${user.lastName}`} key={user.id} src={user.avatarUrl} radius="xl" />
                        ))}
                        </Group>
                    </Stack>
                    <Group justify="flex-end">

                    <Button  color="blue" onClick={ handleSaveTask } loading={TaskUpdating }>Save</Button>
                    </Group>
                </Stack>
        </Flex>
    )
}