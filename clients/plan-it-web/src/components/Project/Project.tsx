import classes from './Project.module.css';
import { Avatar, Group, Progress, Stack, Title, Text, ActionIcon } from "@mantine/core";
import { Handle, } from "../SortableItems/Item";
import { useDisclosure } from '@mantine/hooks';
import { IconAdjustments } from '@tabler/icons-react';
import { ProjectSettings } from './ProjectSettings';
import { ExtendedModal } from '../Common/ExtendedModal';

const avatars = [
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-2.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-4.png',
    'https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-7.png',
  ];

interface ProjectProps {
    handleProps : React.HTMLAttributes<unknown> | undefined;
    onUpdate: () => void;
    onRemove: () => void;
    name: string;
    id: string;
    description: string;
}

export function Project({ onUpdate, onRemove, handleProps, name, description, id } : ProjectProps ) {
    const [modalOpened, { open, close }] = useDisclosure(false);

  return (
    <>
        <ExtendedModal title="Project Settings" opened={modalOpened} onClose={close} >
            <ProjectSettings onUpdate={onUpdate} onRemove={onRemove} projectId={id} />
        </ExtendedModal>
        <Stack className={classes.headerContainer} align="stretch" >
        <Group justify="space-between">
        <Title order={3} >{name} </Title>
            <Group>
                <ActionIcon variant='transparent' onClick={open} >
                    <IconAdjustments />
                </ActionIcon>
                <Handle {...handleProps} />
            </Group>
        </Group>
        <Progress autoContrast value={(23 / 36) * 100} mt={5} />
        <Group justify="space-between">
        <Text fz="sm" mt="md">
        Tasks completed:{' '}
            <Text span fw={500} c="bright">
                23/36
            </Text>
        </Text>
        <Avatar.Group>
            <Avatar src={avatars[0]} radius="xl" />
            <Avatar src={avatars[1]} radius="xl" />
            <Avatar src={avatars[2]} radius="xl" />
            <Avatar radius="xl">+5</Avatar>
        </Avatar.Group>
        </Group>
    </Stack>
    </>
  );
}