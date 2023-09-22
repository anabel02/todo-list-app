import { Button, Checkbox, Input, Table, Text, Tooltip } from '@mantine/core';

import React from 'react';
import { Todo } from '../types/type';
import { IconPencil, IconTrash } from '@tabler/icons-react';

export const ToDoList: ({ todos }: { todos: Todo[]; }) => any = ({ todos }) => {
  const isComplete = (todo: Todo) : boolean => todo.completedDateTime === null;
    return (
        <Table>
        <Table.Tbody>
        { todos.map((todo) => (
          <Table.Tr key={ todo.id }>

            <Table.Td>
              <Checkbox onChange={(event) => console.log} />
            </Table.Td>

            <Table.Td>
              <Tooltip label={"completed \n created"}>
                  <Text size='md' mt='sm'> Hello! This is an example task. </Text>
              </Tooltip>
            </Table.Td>

            <Table.Td>
              <Button variant="filled" rightSection={<IconTrash size={14} />} color="rgba(212, 61, 61, 1)" radius="lg">
                  Delete
              </Button>
            </Table.Td>

            <Table.Td>
              {
                  !isComplete(todo) && 
                      <Input rightSection={<IconPencil size={14} />}
                      placeholder="Edit task"
                      radius="xl"
                      onSubmit={() => (console.log)}
                    />
              }
            </Table.Td>
          </Table.Tr> ))}
        </Table.Tbody>
      </Table>
  );
}
