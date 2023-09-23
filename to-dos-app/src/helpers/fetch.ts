const baseUrl = "http://localhost:5028";

export enum HttpMethod {
  POST = "POST",
  PUT = "PUT",
  PATCH = "PATCH",
  DELETE = "DELETE"
};

export const queryFetch = (endpoint: string): Promise<Response> => fetch(`${baseUrl}/ToDos${endpoint}`);

export const getSortedCompletedTodos : Promise<Response> = queryFetch(`?$filter=not(CompletedDateTime eq null)&&$orderby=(CompletedDateTime) desc`);

export const getSortedNotCompletedTodos : Promise<Response> = queryFetch(`?$filter=(CompletedDateTime eq null)&&$orderby=(CreatedDateTime) asc`);

export const commandFetch = (
    endpoint: string,
    data: any,
    method: HttpMethod
  ): Promise<Response> => {
    const url = `${baseUrl}/Command${endpoint}`;
    return fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
  };