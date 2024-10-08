/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unsafe-assignment */

import {
    UnstyledButton,
    Badge,
    Text,
    Group,
    ActionIcon,
    Tooltip,
    Loader,
  } from '@mantine/core';
  import { IconPlus, IconClipboard, IconLogout} from '@tabler/icons-react';
  import { UserButton } from '../UserButton/UserButton';
  import classes from './Navbar.module.css';
import { NavLink, useNavigate } from 'react-router-dom';
import {  useCreateWorkspaceMutation } from '../../services/planit-api';
import { useAppDispatch, useAppSelector } from '../../hooks/reduxHooks';
import { addWorkspace } from '../../redux/workspacesSlice';
import { logOut } from '../../redux/authSlice';

  export function Navbar() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
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

    const handleUserButtonClick = () => {
      navigate('/profile');
    }

    const links: { icon: any; label: string; notifications?: number, onClick?: () => void }[] = [
      // { icon: IconBulb, label: 'Activity'}, To be implemented
      // { icon: IconCheckbox, label: 'Assigned Tasks' },
      { icon: IconLogout, label: 'Log out', onClick: () => dispatch(logOut()) },
    ];

    const mainLinks = links.map((link) => (
      <UnstyledButton key={link.label} className={classes.mainLink} onClick={link.onClick}>
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
          <Group>
            <IconClipboard size={15} /> <span>{workspace.name}</span>
          </Group>
          
        </NavLink>
      ))
    : null;
  
    return (
      <nav className={classes.navbar}>
        <div className={classes.section}>
          <UserButton onClick={handleUserButtonClick}/>
        </div>  
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