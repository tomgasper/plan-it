import {useSortable} from '@dnd-kit/sortable';
import {CSS} from '@dnd-kit/utilities';

interface SortableItemProps {
  id: number;
}

export function Project( {id} : SortableItemProps) {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
  } = useSortable({id: id});
  
  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };
  
  return (
    <div className="sortable-item" ref={setNodeRef} style={style} {...attributes} {...listeners}>
     hello {id}
    </div>
  );
}