import { List } from '@mantine/core';

import React from 'react'
import { ToDoListItem } from './ToDoListItem';
import { Todo } from '../types/type';
import { isDataView, isDate } from 'util/types';

export const ToDoList: React.FC<{todos: Todo[]}> = ({ todos }) => {
    return (
        <List
          spacing="xs"
          size="sm"
          center
        > { todos.map(todo => <ToDoListItem key={ todo.id } 
                                            todo={ todo }/>)}
        </List>
    );
}
