import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import Root from "./components/NavigationBar/navigationBar.component";
import ErrorPage from "./components/ErrorPage/errorPage.component";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import UserForm, {
  userFormType,
} from "./components/UserForm/userForm.component";
import ProtectedWrap from "./components/ProtectedWrap/protectedWrap.component";
import TrackedProducts from "./components/ProductsTable/trackedProducts.component";
import About from "./components/About/about.component";
import ProductInfo from "./components/ProductInfo/productInfo.component";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    children: [
      {
        path: "/",
        element: (
          <ProtectedWrap>
            <TrackedProducts />
          </ProtectedWrap>
        ),
      },
      {
        path: "/:id",
        element: (
          <ProtectedWrap>
            <ProductInfo />
          </ProtectedWrap>
        ),
      },
      {
        path: "/about",
        element: <About />,
      },
    ],
    errorElement: <ErrorPage />,
  },
  {
    path: "/login",
    element: <UserForm formType={userFormType.login} />,
  },
  {
    path: "register",
    element: <UserForm formType={userFormType.register} />,
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
