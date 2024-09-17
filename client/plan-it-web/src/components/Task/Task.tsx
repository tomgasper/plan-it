import classes from './Task.module.css';
import type { Task } from '../../types/Task';
import { Card, Text, Badge, Group, ActionIcon, useMantineTheme, Stack, Title} from '@mantine/core';
import { IconSettings, IconX } from '@tabler/icons-react';
import { useDeleteProjectTaskMutation } from '../../services/planit-api';
import { notifications } from '@mantine/notifications';
import { useDisclosure } from '@mantine/hooks';
import { ExtendedModal } from '../Common/ExtendedModal';
import { TaskSettings } from './TaskSettings';
import { getDifferenceInDays } from '../../utils/utils';


interface SortableItemProps {
  id: string;
  taskContent : Task;
  onDelete: () => void;
  onUpdate: () => void;
}

export function Task( {id, projectId, taskContent, onDelete, onUpdate} : SortableItemProps) {    
  const [deleteProjectTask] = useDeleteProjectTaskMutation();
  const [modalOpened, { open, close }] = useDisclosure(false);
  
  const theme = useMantineTheme();

  const handleDelete = async () => {
    console.log('Deleting project task:', id);
    const result = await deleteProjectTask({projectId, taskId:id});

    if (result.error)
    {
      console.error('Error deleting project task:', result.error);
      notifications.show({
        title: 'Error deleting project task',
        message: 'Could not delete project task, please try again!',
        color: 'red'
      });
      return;
    }

    // Callback from top component
    onDelete();
    notifications.show({
      title: 'Project task deleted',
      message: 'Project task was successfully deleted',
      color: 'green'
    });
  }

  return (
    <>
    <ExtendedModal opened={modalOpened} onClose={close} title="Task Settings" >
      <TaskSettings closeModal={close} onUpdate={onUpdate} projectId={projectId} taskId={id} assignedUsers={taskContent.taskWorkers} />
     </ExtendedModal>
    <Card pt={0} pl={10} pb={15} className={classes.container} >
      <Stack gap={3}>
        <Group justify='space-between' mt={10}>
          <Group>
              <Title order={5} >{taskContent.name}</Title>
          </Group>
          <Group gap={4}>
                <ActionIcon variant='transparent' color='blue' size={22} onMouseDown={open}>
                  <IconSettings stroke={2.2} />
                </ActionIcon>
              <ActionIcon variant='transparent' color='red' size={22} onMouseDown={handleDelete}>
                <IconX stroke={2.7} />
                </ActionIcon>
            </Group>
        </Group>
      <Text c={"#545454"} fz="sm" mt={5}>
        {taskContent.description}
      </Text>

      <Group mt={10}>
        <Badge>{ getDifferenceInDays(taskContent.dueDate, new Date()) } days left</Badge>
        <Badge color={theme.colors.orange[4]}>Medium priority</Badge>
      </Group>
      </Stack>
    </Card>
    </>
    )
}