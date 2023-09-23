const baseUrl = "http://localhost:5028";

export enum HttpMethod {
  POST = "POST",
  PUT = "PUT",
  PATCH = "PATCH",
  DELETE = "DELETE"
};

export const queryFetch = (endpoint: string): Promise<Response> => fetch(`${baseUrl}/ToDos${endpoint}`);

export const getSortedCompletedTodos : Promise<Response> = queryFetch(`?$filter=not(CompletedDateTime eq null)&&$orderby=(CompletedDateTime) desc`);

//Filter movies that have the word Super in its title: http://host/service/Movies?$filter=contains(@word,Title)&@word='Super'

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