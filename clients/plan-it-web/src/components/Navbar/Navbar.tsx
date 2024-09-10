/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unsafe-assignment */

import {
    TextInput,
    Code,
    UnstyledButton,
    Badge,
    Text,
    Group,
    ActionIcon,
    Tooltip,
    Loader,
  } from '@mantine/core';
  import { IconBulb, IconUser, IconCheckbox, IconSearch, IconPlus} from '@tabler/icons-react';
  import { UserButton } from '../UserButton/UserButton';
  import classes from './Navbar.module.css';
import { NavLink } from 'react-router-dom';
import {  useCreateWorkspaceMutation } from '../../services/planit-api';
import { useAppDispatch, useAppSelector } from '../../hooks/reduxHooks';
import { addWorkspace } from '../../redux/workspacesSlice';
  
  
  const links: { icon: any; label: string; notifications?: number }[] = [
    { icon: IconBulb, label: 'Activity', notifications: 3 },
    { icon: IconCheckbox, label: 'Tasks', notifications: 4 },
    { icon: IconUser, label: 'Contacts' },
  ];
  

  
  export function Navbar() {
    const dispatch = useAppDispatch();
    const workspaces = useAppSelector( state => state.workspaces.workspaces);
    const [ createWorkspace ]= useCreateWorkspaceMutation();

    const handleClickIconPlus = async () => {
      const newWorkspace = await createWorkspace({
        name: "New Workspace",
        description: "",
        projectIds: []
      });

      if (!newWorkspace.data) return;

      dispatch(addWorkspace(newWorkspace.data));
    }

    const mainLinks = links.map((link) => (
      <UnstyledButton key={link.label} className={classes.mainLink}>
        <div className={classes.mainLinkInner}>
          <link.icon size={20} className={classes.mainLinkIcon} stroke={1.5} />
          <span>{link.label}</span>
        </div>
        {link.notifications && (
          <Badge size="sm" variant="filled" className={classes.mainLinkBadge}>
            {link.notifications}
          </Badge>
        )}
      </UnstyledButton>
    ));
  
    const workspacesLinks = workspaces
    ? workspaces.map((workspace) => (
        <NavLink
          to={`/workspaces/${workspace.id}`}
          key={workspace.id}
          className={({ isActive }) =>
            `${classes.workspaceLink} ${isActive ? classes.workspaceLinkActive : ''}`
          }
        >
          <span>{workspace.name}</span>
        </NavLink>
      ))
    : null;
  
    return (
      <nav className={classes.navbar}>
        <div className={classes.section}>
          <UserButton />
        </div>
  
        <TextInput
          placeholder="Search"
          size="xs"
          leftSection={<IconSearch style={{ width: String(12), height: String(12) }} stroke={1.5} />}
          rightSectionWidth={70}
          rightSection={<Code className={classes.searchCode}>Ctrl + K</Code>}
          styles={{ section: { pointerEvents: 'none' } }}
          mb="sm"
        />
  
        <div className={classes.section}>
          <div className={classes.mainLinks}>{mainLinks}</div>
        </div>
  
        <div className={classes.section}>
          <Group className={classes.workspacesHeader} justify="space-between">
            <Text size="xs" fw={500} c="dimmed">
              Workspaces
            </Text>
            <Tooltip label="Create workspace" withArrow position="right">
              <ActionIcon variant="default" size={18} onClick={handleClickIconPlus}>
                <IconPlus style={{ width: String(12), height: String(12) }} stroke={1.5} />
              </ActionIcon>
            </Tooltip>
          </Group>
          <div className={classes.workspaces}>{!workspacesLinks ? <Loader color="blue" /> : workspacesLinks}</div>
        </div>
      </nav>
    );
  }