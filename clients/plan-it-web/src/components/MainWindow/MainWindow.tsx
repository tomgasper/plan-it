import { closestCenter, DndContext, DragEndEvent } from "@dnd-kit/core";
import { arrayMove, rectSortingStrategy, SortableContext } from "@dnd-kit/sortable";
import { useState } from "react";
import { Project } from "../Project/Project";
import classes from "./MainWindow.module.css";

export function MainWindow()
{
    const [items, setItems] = useState([1, 2, 3]);
    
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

    return (
        <div className={classes.container}>
            <DndContext
                collisionDetection={closestCenter}
                onDragEnd={handleDragEnd}
                >
                <SortableContext
                items={items}
                strategy={rectSortingStrategy}
                >
                    {items.map((item) => (
                    <Project key={item} id={item} />
                    ))}
                </SortableContext>
        </DndContext>
        </div>
    )
}