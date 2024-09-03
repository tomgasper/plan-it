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
  } from '@mantine/core';
  import { IconBulb, IconUser, IconCheckbox, IconSearch, IconPlus } from '@tabler/icons-react';
  import { UserButton } from '../UserButton/UserButton';
  import classes from './Navbar.module.css';
  
  
  const links: { icon: any; label: string; notifications?: number }[] = [
    { icon: IconBulb, label: 'Activity', notifications: 3 },
    { icon: IconCheckbox, label: 'Tasks', notifications: 4 },
    { icon: IconUser, label: 'Contacts' },
  ];
  
  const collections = [
    { emoji: '👍', label: 'Sales' },
    { emoji: '🚚', label: 'Deliveries' },
    { emoji: '💸', label: 'Discounts' },
    { emoji: '💰', label: 'Profits' },
    { emoji: '✨', label: 'Reports' },
    { emoji: '🛒', label: 'Orders' },
    { emoji: '📅', label: 'Events' },
    { emoji: '🙈', label: 'Debts' },
    { emoji: '💁‍♀️', label: 'Customers' },
  ];
  
  export function Navbar() {
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
  
    const collectionLinks = collections.map((collection) => (
      <a
        href="#"
        onClick={(event) => event.preventDefault()}
        key={collection.label}
        className={classes.collectionLink}
      >
        <span style={{ marginRight: String(9), fontSize: String(16) }}>{collection.emoji}</span>{' '}
        {collection.label}
      </a>
    ));
  
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
          <Group className={classes.collectionsHeader} justify="space-between">
            <Text size="xs" fw={500} c="dimmed">
              Workspaces
            </Text>
            <Tooltip label="Create collection" withArrow position="right">
              <ActionIcon variant="default" size={18}>
                <IconPlus style={{ width: String(12), height: String(12) }} stroke={1.5} />
              </ActionIcon>
            </Tooltip>
          </Group>
          <div className={classes.collections}>{collectionLinks}</div>
        </div>
      </nav>
    );
  }