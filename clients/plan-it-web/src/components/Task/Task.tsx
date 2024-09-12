import { TaskCard } from './TaskCard';
import classes from './Task.module.css';

interface SortableItemProps {
  id: string;
  projectId: string;
  name: string;
  description: string;
  onDelete: () => void;
  onUpdate: () => void;
}

export function Task( {id, projectId, name, description, onDelete, onUpdate} : SortableItemProps) {
  return (
    <div className={classes.container} key={id} id={id}>
      
     <TaskCard onUpdate={onUpdate} projectId={projectId} taskId={id} name={name} description={description} onDelete={onDelete}/>
    </div>
  );
}