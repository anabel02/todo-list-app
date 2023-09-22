import React from 'react'
import { Box, Button, ListItem, Text, ThemeIcon } from '@mantine/core';
import { IconCircleCheck, IconCircleDashed, IconPencil, IconTrash } from '@tabler/icons-react';
import { ITodo } from '../types/type';

export const ToDoListItem : React.FC<{todo: ITodo}> = ({ todo }) => {
    const isComplete = (todo: ITodo) : boolean => todo.completedDateTime === null;

    return (
        <ListItem icon={ 
            isComplete(todo) ?
            <ThemeIcon color="teal" size={24} radius="xl">
            <IconCircleCheck size="1rem" />
            </ThemeIcon> 
            :
            <ThemeIcon color="blue" size={24} radius="xl">
            <IconCircleDashed size="1rem" />
        </ThemeIcon>
        }>
            <Text>
                { todo.task }
            </Text>

            <Button variant="filled" rightSection={<IconTrash size={14} />} color="rgba(212, 61, 61, 1)" radius="lg">
                Delete
            </Button>

            {
                !isComplete(todo) ?
                <Button variant="filled" rightSection={<IconPencil size={14} />} color="rgba(82, 162, 186, 1)" radius="lg">
                    Update
                </Button>
                :
                <Button variant="filled" disabled rightSection={<IconPencil size={14} />} color="rgba(82, 162, 186, 1)" radius="lg">
                Update
                </Button>
            }
            ;  
        </ListItem>
    );
}
