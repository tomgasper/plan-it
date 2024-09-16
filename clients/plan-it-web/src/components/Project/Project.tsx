import classes from './Project.module.css';
import { Avatar, Group, Progress, Stack, Title, Text, ActionIcon } from "@mantine/core";
import { Handle, } from "../SortableItems/Item";
import { useDisclosure } from '@mantine/hooks';
import { IconAdjustments } from '@tabler/icons-react';
import { ProjectSettings } from './ProjectSettings';
import { ExtendedModal } from '../Common/ExtendedModal';
import { User } from '../../types/User';
import { ProjectWithDetails } from '../../types/Project';
import { ProjectAssignedUsers } from './ProjectAssignedUsers';

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
        <Group justify="space-between" align='center'>
        <Text fz="sm" mt="md">
        Tasks completed:{' '}
            <Text span fw={500} c="bright">
                {project.projectTasks.filter(task => task.isCompleted).length}/{project.projectTasks.length}
            </Text>
        </Text>
            <ProjectAssignedUsers project={project} />
        </Group>
    </Stack>
    </>
  );
}