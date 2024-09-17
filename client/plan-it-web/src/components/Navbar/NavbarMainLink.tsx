import classes from './Navbar.module.css';

export function NavbarMainLink({icon, label, notifications}: {icon: JSX, label: string, notifications?: number}) {
  return (
    <div className={classes.mainLink}>
      <div className={classes.mainLinkInner}>
        <icon size={20} className={classes.mainLinkIcon} stroke={1.5} />
        <span>{link.label}</span>
      </div>
      {link.notifications && (
        <Badge size="sm" variant="filled" className={classes.mainLinkBadge}>
          {link.notifications}
        </Badge>
      )}
    </div>
  );
}