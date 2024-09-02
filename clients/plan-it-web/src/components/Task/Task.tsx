import {useSortable} from '@dnd-kit/sortable';
import {CSS} from '@dnd-kit/utilities';
import { TaskCard } from './TaskCard';
import classes from './Task.module.css';

interface SortableItemProps {
  id: number;
  key: number;
}

export function Task( {id, key} : SortableItemProps) {
  
  return (
    <div className={classes.container} key={key} id={id.toString()}>
     <TaskCard id={id} />
    </div>
  );
}