import { TextInput, Button, Group, Box } from '@mantine/core';
import { useForm } from '@mantine/form';
import { useState } from 'react';
import { useAppDispatch } from '../store/store';
import { addTodo } from '../store/actionsCreator';

export const AddToDo = () => {
  const dispatch = useAppDispatch();

  const [newTodo, setNewTodo] = useState("");

  const handleAddTodo = () => {
    if (newTodo.trim().length == 0){
      console.log(`Task text mustn't be empty`)
      return;
    };
    dispatch(addTodo(newTodo, new Date()));
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