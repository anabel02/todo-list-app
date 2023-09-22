import { TextInput, Button, Group, Box } from '@mantine/core';
import { useForm } from '@mantine/form';
import { Todo } from '../types/type';

export const AddToDo : React.FC<{callback: (value: string) => void}>= ({ callback }) => {
    const form = useForm({
        initialValues: {
          task: ''
        }, validate: {
            task: (value) => (value.trim().length > 0 ? null : `Task text mustn't be empty`),
          },
        });

        return (
            <Box maw={340} mx="auto">
              <form onSubmit={form.onSubmit(({task}) => callback(task))}>
                <TextInput
                  placeholder="New To Do..."
                  {...form.getInputProps('task')}
                />
        
                <Group justify="flex-end" mt="md">
                  <Button type="submit">Add To Do</Button>
                </Group>
              </form>
            </Box>
          );
}