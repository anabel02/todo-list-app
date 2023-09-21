import { TextInput, Button, Group, Box } from '@mantine/core';
import { useForm } from '@mantine/form';

export const AddToDo = (handleAdd: Function) => {
    const form = useForm({
        initialValues: {
          task: ''
        }, validate: {
            task: (value) => (value.trim().length > 0 ? null : `Task text mustn't be empty`),
          },
        });

        return (
            <Box maw={340} mx="auto">
              <form onSubmit={form.onSubmit((values) => handleAdd())}>
                <TextInput
                  placeholder="New task"
                  {...form.getInputProps('task')}
                />
        
                <Group justify="flex-end" mt="md">
                  <Button type="submit">Submit</Button>
                </Group>
              </form>
            </Box>
          );
}