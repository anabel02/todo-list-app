const baseUrlQueries = "http://localhost:5028/ToDos";
const baseUrlCommands = "http://localhost:5028/Command";

export enum HttpMethod {
  POST = "POST",
  PUT = "PUT",
  PATCH = "PATCH",
  DELETE = "DELETE"
};

export const queryFetch = (endpoint: string): Promise<Response> => fetch(`${baseUrlQueries}${endpoint}`);

export const commandFetch = (
    endpoint: string,
    data: any,
    method: HttpMethod
  ): Promise<Response> => {
    const url = `${baseUrlCommands}/${endpoint}`;
    return fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
  };