import './App.css';
import { useFetchToDos } from './hooks/useFetch';
import { ToDoList } from './components/ToDoList';
import { Todo } from './types/todo';

const baseUrl = "http://localhost:5028/ToDos"
// const httpClient = new FetchClient();
// const service = new ToDoService(httpClient, baseUrl, "");

// const builder = async () => await service.query((builder, qtodo) => builder);

function App() {
  const completedFilter = `?$filter=not(CompletedDateTime eq null)`;
  const notCompletedFilter = `?$filter=(CompletedDateTime eq null)`;

  const completedTodos : Todo[] = useFetchToDos(completedFilter);
  const notCompletedTodos : Todo[] = useFetchToDos(notCompletedFilter);
  console.log(completedTodos);
  console.log(notCompletedTodos);
  return (<>
  </>
  );
}

export default App;
