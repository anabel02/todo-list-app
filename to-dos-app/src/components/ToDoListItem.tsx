import React from 'react'
import { Button, Checkbox, Flex, Grid, Input, ListItem, Text, Tooltip} from '@mantine/core';
import { IconCircleCheck, IconCircleDashed, IconPencil, IconTrash } from '@tabler/icons-react';
import { Todo } from '../types/type';

export const ToDoListItem : React.FC<{todo: Todo}> = ({ todo }) => {
    const isComplete = (todo: Todo) : boolean => todo.completedDateTime === null;
    console.log(todo);
    return (
        <ListItem >
            <Checkbox onChange={(event) => console.log} />

            <Tooltip label={"completed \n created"}>
                <Text size='md' mt='sm'> task </Text>
            </Tooltip>
            
            <Button variant="filled" rightSection={<IconTrash size={14} />} color="rgba(212, 61, 61, 1)" radius="lg">
                Delete
            </Button>
        
            {
                !isComplete(todo) && 
                    <Input component="button" pointer onSubmit={() => console.log}>
                        <Input.Placeholder>Edit</Input.Placeholder>
                    </Input>
            }
        </ListItem>
    );
}
