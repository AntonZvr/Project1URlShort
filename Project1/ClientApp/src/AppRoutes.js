import { LoginPage } from "./components/LoginPage";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/LoginPage',
    element: <LoginPage />
  }
];

export default AppRoutes;
