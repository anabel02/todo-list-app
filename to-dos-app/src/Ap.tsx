import {
	Button,
	Container,
	Text,
	Title,
	Modal,
	TextInput,
	Group,
	Card,
	ActionIcon
} from '@mantine/core';
import { IconTrash } from '@tabler/icons-react';
import { useState, useEffect} from 'react';

interface Task{
  title: string,
  summary: string
}

export default function Ap() {
	const [tasks, setTasks] = useState<Task[]>([]);
	const [opened, setOpened] = useState(false);
  const [taskTitle, setTaskTitle] = useState("");
  const [taskSummary, setSummary] = useState("");

	function createTask() {
		setTasks([
			...tasks,
			{
				title: taskTitle,
				summary: taskSummary,
			},
		]);

		saveTasks([
			...tasks,
			{
				title: taskTitle,
				summary: taskSummary,
			},
		]);
	}

	function deleteTask(index: number) {
		var clonedTasks = [...tasks];

		clonedTasks.splice(index, 1);

		setTasks(clonedTasks);

		saveTasks([...clonedTasks]);
	}

	async function loadTasks() {
    // let todos = (await Client.toDos().query()).data.value;

  //  let tasks = todos.map((todo,index) => {return {title: todo.id.toString(), summary: todo.task}});

  //   setTasks(tasks)

		let loadedTasks = localStorage.getItem('tasks');

		let tasks = JSON.parse(loadedTasks ?? "null");

		if (tasks) {
			setTasks(tasks);
		}
	}

	function saveTasks(tasks: any) {
		localStorage.setItem('tasks', JSON.stringify(tasks));
	}
	useEffect(() => {
		loadTasks();
	}, []);

	return (
				<div className='App'>
					<Modal
						opened={opened}
						size={'md'}
						title={'New Task'}
						withCloseButton={false}
						onClose={() => {
							setOpened(false);
						}}
						centered>
						<TextInput
							mt={'md'}
							value={taskTitle}
              onChange={(arg) => setTaskTitle(arg.target.value)}
							placeholder={'Task Title'}
							required
							label={'Title'}
						/>
						<TextInput
							value={taskSummary}
              onChange={(arg) => setSummary(arg.target.value)}
							mt={'md'}
							placeholder={'Task Summary'}
							label={'Summary'}
						/>
						<Group mt={'md'}>
							<Button
								onClick={() => {
									setOpened(false);
								}}
								variant={'subtle'}>
								Cancel
							</Button>
							<Button
								onClick={() => {
									createTask();
									setOpened(false);
								}}>
								Create Task
							</Button>
						</Group>
					</Modal>
					<Container size={550} my={40}>
						<Group>
							<Title>
								My Tasks
							</Title>
						</Group>
						{tasks.length > 0 ? (
							tasks.map((task, index) => {
								if (task.title) {
									return (
										<Card withBorder key={index} mt={'sm'}>
											<Group>
												<Text>{task.title}</Text>
												<ActionIcon
													onClick={() => {
														deleteTask(index);
													}}
													color={'red'}
													variant={'transparent'}>
													<IconTrash />
												</ActionIcon>
											</Group>
											<Text color={'dimmed'} size={'md'} mt={'sm'}>
												{task.summary
													? task.summary
													: 'No summary was provided for this task'}
											</Text>
										</Card>
									);
								}
							})
						) : (
							<Text size={'lg'} mt={'md'} color={'dimmed'}>
								You have no tasks
							</Text>
						)}
						<Button
							onClick={() => {
								setOpened(true);
							}}
							fullWidth
							mt={'md'}>
							New Task
						</Button>
					</Container>
				</div>
	);
}