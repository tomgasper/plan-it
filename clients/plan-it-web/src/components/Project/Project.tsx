import classes from './Project.module.css';
import { Avatar, Group, Progress, Stack, Title, Text, ActionIcon } from "@mantine/core";
import { Handle, } from "../SortableItems/Item";
import { useDisclosure } from '@mantine/hooks';
import { IconAdjustments } from '@tabler/icons-react';
import { ProjectSettings } from './ProjectSettings';
import { ExtendedModal } from '../Common/ExtendedModal';
import { User } from '../../types/User';
import { ProjectWithDetails } from '../../types/Project';

interface ProjectProps {
    handleProps : React.HTMLAttributes<unknown> | undefined;
    onUpdate: () => void;
    onRemove: () => void;
    id: string;
    project : ProjectWithDetails;
}

export function Project({ onUpdate, onRemove, handleProps, id, project } : ProjectProps ) {
    // use app logic not server side
    // const { data: project, error: projectError, isLoading: projectLoading, refetch } = useGetProjectWithDetailsQuery(id);
    const [modalOpened, { open, close }] = useDisclosure(false);

    const getUsersFromAllTasks = (maxNumOfUsers = 5) : User[] => {
        if (!project?.projectTasks) return [];

        const users : User[] = [];
        const userIds = new Set<string>(); 
    
        for (const task of project.projectTasks) {
            if (task.taskWorkers === null) continue;
            for (const user of task.taskWorkers) {
                if (userIds.size >= maxNumOfUsers) break;
                if (userIds.has(user.id)) continue;
                userIds.add(user.id);
                users.push(user);
            }
        }

        return users;
    }

    const numOfUsersAssignedToTasks = () : number => {
        if (!project?.projectTasks) return 0;

        const users = new Set<string>();
        for (const task of project.projectTasks) {
            if (task.taskWorkers === null) continue;
            for (const user of task.taskWorkers) {
                users.add(user.id);
            }
        }

        return users.size;
    }

    const MAX_NUM_AVATARS_TO_DISPLAY = 5;
    const users = getUsersFromAllTasks(MAX_NUM_AVATARS_TO_DISPLAY);
    const restNumOfUsers = numOfUsersAssignedToTasks() - users.length;

  return (
    <>
        <ExtendedModal title="Project Settings" opened={modalOpened} onClose={close} >
            <ProjectSettings onUpdate={onUpdate} onRemove={onRemove} projectId={id} />
        </ExtendedModal>
        <Stack className={classes.headerContainer} align="stretch" >
        <Group justify="space-between">
        <Title order={3} >{project.name} </Title>
            <Group>
                <ActionIcon variant='transparent' onClick={open} >
                    <IconAdjustments />
                </ActionIcon>
                <Handle {...handleProps} />
            </Group>
        </Group>
        <Progress autoContrast value={(0 * 100)} mt={5} />
        <Group justify="space-between">
        <Text fz="sm" mt="md">
        Tasks completed:{' '}
            <Text span fw={500} c="bright">
                {project.projectTasks.filter(task => task.isCompleted).length}/{project.projectTasks.length}
            </Text>
        </Text>
        <Avatar.Group>
            {users.map((user : User) => (
                <Avatar key={user.id} src={user.avatarUrl} radius="xl" />
            ))}
            { restNumOfUsers > 0 && <Avatar radius="xl">{restNumOfUsers}</Avatar>}
        </Avatar.Group>
        </Group>
    </Stack>
    </>
  );
}