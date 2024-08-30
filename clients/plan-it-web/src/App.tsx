import {useState} from 'react';
import {closestCenter, DndContext, DragEndEvent} from '@dnd-kit/core';

import { arrayMove, SortableContext, verticalListSortingStrategy } from '@dnd-kit/sortable';
import { SortableItem } from './SortableItem';

export default function App() {
  const [items, setItems] = useState(['1', '2', '3']);

  return (
    <DndContext
    collisionDetection={closestCenter}
    onDragEnd={handleDragEnd}
    >
      <SortableContext
      items={items}
      strategy={verticalListSortingStrategy}
      >
        {items.map((item) => (
          <SortableItem key={item} id={item} />
        ))}
      </SortableContext>
    </DndContext>
  );

  function handleDragEnd(event : DragEndEvent) {
    const {active, over} = event;
    
    if (active.id !== over.id) {
      setItems((items) => {
        const oldIndex = items.indexOf(active.id);
        const newIndex = items.indexOf(over.id);
        
        return arrayMove(items, oldIndex, newIndex);
      });
    }
  }
};