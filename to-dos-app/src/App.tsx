import './App.css';
import { ToDoList } from './components/ToDoList';
import { useFetchToDos } from './hooks/useFetch';

function App() {
  const completedFilter = `?$filter=not(CompletedDateTime eq null)`;
  const notCompletedFilter = `?$filter=(CompletedDateTime eq null)`;

  const completedTodos = useFetchToDos(completedFilter);
  const notCompletedTodos = useFetchToDos(notCompletedFilter);
  
  return (
  <>
  </>
  );
}

export default App;
