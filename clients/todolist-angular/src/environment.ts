interface Environment {
  production: boolean;
  apiBaseUrl: string;
  authApiBaseUrl: string;
}

export const environment: Environment = {
  production: false,
  apiBaseUrl: "http://localhost:5028",
  authApiBaseUrl: "http://localhost:5103"
};
