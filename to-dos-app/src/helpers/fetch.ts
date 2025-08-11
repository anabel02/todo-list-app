const baseUrl = process.env.REACT_APP_API_URL;

export enum HttpMethod {
  POST = "POST",
  PUT = "PUT",
  DELETE = "DELETE"
};

export const queryFetch = (endpoint: string): Promise<Response> => fetch(`${baseUrl}/ToDos${endpoint}`);

export const getSortedCompletedTodos = (): Promise<Response> => queryFetch(`?Sorting.OrderBy=CompletedDateTime&Sorting.Descending=false&Filter.FilterString=CompletedDateTime%20ne%20null`);

export const getSortedNotCompletedTodos = (): Promise<Response> => queryFetch(`?Sorting.OrderBy=CompletedDateTime&Sorting.Descending=true&Filter.FilterString=CompletedDateTime%20eq%20null`);

export const commandFetch = (
    endpoint: string,
    data: any,
    method: HttpMethod
  ): Promise<Response> => {
    const url = `${baseUrl}/ToDos${endpoint}`;
    return fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
  };