import { TextInput, Button, Group, Box } from '@mantine/core';
import { useState } from 'react';
import { useAppDispatch } from '../store/store';
import { addTodo } from '../store/actionsCreator';
import { notifications } from '@mantine/notifications';

export const AddToDo = () => {
  const dispatch = useAppDispatch();

  const [newTodo, setNewTodo] = useState("");

  const handleAddTodo = () => {
    if (newTodo.trim().length == 0){
      notifications.show({
        color: 'red',
        title: 'Invalid task',
        message: `Task text mustn't be empty`});
      return;
    };

    dispatch(addTodo(newTodo));
    setNewTodo("");
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    setNewTodo(e.target.value);
  };

  return (
    <Box maw={340} h={100} mx="auto">
      <form onSubmit={handleAddTodo}>
        <TextInput
          onChange={handleInputChange}
          value={newTodo}
        />

        <Group justify="flex-end" mt="md">
          <Button type="submit">Add To Do</Button>
        </Group>
      </form>
    </Box>
  );
}