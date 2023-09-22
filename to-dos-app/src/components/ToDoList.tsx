import { List } from '@mantine/core';

import React from 'react'
import { ToDoListItem } from './ToDoListItem';
import { ITodo } from '../types/type';

export const ToDoList: React.FC<{todos: ITodo[]}> = ({ todos }) => {
    return (
        <List
          spacing="xs"
          size="sm"
          center
        > { todos.map(todo => <ToDoListItem key={todo.id} 
                                            todo={todo}/>)}
        </List>
    );
}