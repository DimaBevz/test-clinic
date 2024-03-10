import * as React from "react";
import * as ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ChakraProvider } from "@chakra-ui/react";
import "./index.css";
import ProtectedRoute from "./routes/ProtectedRoute.tsx";
import { theme } from "../theme.ts";
import "./i18n.ts";
import { persistor, store } from "@store/index.ts";
import { PersistGate } from "redux-persist/integration/react";
import { Provider } from "react-redux";
import { Role } from "@interfaces/IAuth.ts";
import { MainLayout } from "@components/MainLayout/index.tsx";
import { injectStoreToHttpClient } from "@http-client/index.ts";
import {
  BugPage,
  DoctorInfoPage,
  HelpPage,
  NoAccessPage,
  DoctorsListPage,
  AppointmentCallPage,
  AppointmentFormPage,
  AppointmentInfoPage,
  AppointmentListPage,
  ChangePasswordPage,
  ConfirmUserPage,
  LoginPage,
  RegistrationPage,
  TimeTablePage,
  TestsListPage,
  TestInfoPage,
  LandingPage,
  ProfilePage,
  AboutUs,
  OurTeam,
} from "@pages/index.ts";

injectStoreToHttpClient(store);

const router = createBrowserRouter([
  { index: true, element: <LandingPage /> },
  {
    path: "/403",
    element: <NoAccessPage text="У вас немає доступу до цієї сторінки" />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/registration",
    element: <RegistrationPage />,
  },
  {
    path: "/confirmation",
    element: <ConfirmUserPage />,
  },
  {
    path: "/change-password",
    element: <ChangePasswordPage />,
  },
  {
    path: "/about-us",
    element: <AboutUs />,
  },
  {
    path: "/our-team",
    element: <OurTeam />,
  },
  {
    path: "/",
    element: <MainLayout />,
    children: [
      {
        path: "/profile",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor, Role.Admin]}
            elementsMapping={{
              "0": <ProfilePage />,
              "1": <ProfilePage />,
              "2": <>home admin</>,
            }}
          />
        ),
      },
      {
        path: "/schedule",
        element: (
          <ProtectedRoute
            roles={[Role.Doctor]}
            elementsMapping={{
              "1": <TimeTablePage />,
            }}
          />
        ),
      },
      {
        path: "/appointments",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor]}
            elementsMapping={{
              "0": <AppointmentListPage />,
              "1": <AppointmentListPage />,
            }}
          />
        ),
      },
      {
        path: "/appointments/:id",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor]}
            elementsMapping={{
              "0": <AppointmentInfoPage />,
              "1": <AppointmentInfoPage />,
            }}
          />
        ),
      },
      {
        path: "/appointments/:id/call",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor]}
            elementsMapping={{
              "0": <AppointmentCallPage />,
              "1": <AppointmentCallPage />,
            }}
          />
        ),
      },
      {
        path: "/doctors/:id",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor, Role.Admin]}
            elementsMapping={{
              "0": <DoctorInfoPage />,
              "1": <DoctorInfoPage />,
              "2": <>home admin</>,
            }}
          />
        ),
      },
      {
        path: "/doctors",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor, Role.Admin]}
            elementsMapping={{
              "0": <DoctorsListPage />,
              "1": <DoctorsListPage />,
              "2": <>home admin</>,
            }}
          />
        ),
      },
      {
        path: "/doctors/appointment/:id",
        element: (
          <ProtectedRoute
            roles={[Role.Patient]}
            elementsMapping={{
              "0": <AppointmentFormPage />,
            }}
          />
        ),
      },
      {
        path: "/help",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor, Role.Admin]}
            elementsMapping={{
              "0": <HelpPage />,
              "1": <HelpPage />,
              "2": <HelpPage />,
            }}
          />
        ),
      },
      {
        path: "/report-bug",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor, Role.Admin]}
            elementsMapping={{
              "0": <BugPage />,
              "1": <BugPage />,
              "2": <BugPage />,
            }}
          />
        ),
      },
      {
        path: "/tests",
        element: (
          <ProtectedRoute
            roles={[Role.Patient]}
            elementsMapping={{
              "0": <TestsListPage />,
            }}
          />
        ),
      },
      {
        path: "/tests/:id",
        element: (
          <ProtectedRoute
            roles={[Role.Patient]}
            elementsMapping={{
              "0": <TestInfoPage />,
            }}
          />
        ),
      },
    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <ChakraProvider theme={theme}>
      <Provider store={store}>
        <PersistGate loading={null} persistor={persistor}>
          <RouterProvider router={router} />
        </PersistGate>
      </Provider>
    </ChakraProvider>
  </React.StrictMode>
);
