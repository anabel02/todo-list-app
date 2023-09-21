export const useFetch = (
    endpoint: string,
    data: any,
    method: string = "GET"
  ): Promise<Response> => {
    const url = endpoint;
  
    if (method === "GET") {
      return fetch(url);
    } else {
      return fetch(url, {
        method,
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
    }
  };