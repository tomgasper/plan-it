import React from 'react';
import {useSortable} from '@dnd-kit/sortable';
import {CSS} from '@dnd-kit/utilities';

export function SortableItem(props) {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
  } = useSortable({id: props.id});
  
  const style = {
    transform: CSS.Transform.toString(transform),
    display: "flex",
    width:"200px",
    backgroundColor: "red",
    transition,
  };
  
  return (
    <li ref={setNodeRef} style={style} {...attributes} {...listeners}>
     hello {props.id}
    </li>
  );
}