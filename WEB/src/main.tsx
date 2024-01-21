import * as React from "react";
import * as ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ChakraProvider } from "@chakra-ui/react";
import "./index.css";
import ProtectedRoute from "./routes/ProtectedRoute.tsx";
import { theme } from "../theme.ts";
import "./i18n.ts";
import { LoginForm } from "@pages/Login/Login.tsx";
import { persistor, store } from "@store/index.ts";
import { PersistGate } from "redux-persist/integration/react";
import { Provider } from "react-redux";
import { Role } from "@interfaces/IAuth.ts";
import { ChangePassword } from "@pages/ChangePassword/ChangePassword.tsx";
import { Registration } from "@pages/Registration/Registration.tsx";
import { ProfilePage } from "@pages/Profile/index.tsx";
import { MainLayout } from "@components/MainLayout/index.tsx";
import { injectStoreToHttpClient } from "@http-client/index.ts";
import { ConfirmUser } from "@pages/ConfirmUser/ConfirmUser.tsx";
import { TimeTable } from "@pages/TimeTable/index.tsx";
import { BugPage, DoctorPage, Help, NoAccess } from "@pages/index.ts";
import DoctorsList from "@pages/DoctorsList";
import { AppointmentFormPage } from "@pages/AppointmentFormPage/index.tsx";
import { AppointmentListPage } from "@pages/AppointmentListPage/index.tsx";
import { AppointmentInfoPage } from "@pages/AppointmentInfoPage/index.tsx";
import { AppointmentCall } from "@features/Appointment/AppointmentCall/index.tsx";
import Home from "@pages/Home";
import LandingPage from "@pages/LandingPage";

injectStoreToHttpClient(store);

const router = createBrowserRouter([
  {
    path: "/403",
    element: <NoAccess text="У вас немає доступу до цієї сторінки" />,
  },
	{
		path: "/home",
		element: <LandingPage/>,
	},
  {
    path: "/login",
    element: <LoginForm />,
  },
  {
    path: "/registration",
    element: <Registration />,
  },
  {
    path: "/confirmation",
    element: <ConfirmUser />,
  },
  {
    path: "/change-password",
    element: <ChangePassword />,
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
              "1": <TimeTable />,
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
              "0": <AppointmentCall />,
              "1": <AppointmentCall />,
            }}
          />
        ),
      },
      {
				path: "/doctors/:id",
				element: <ProtectedRoute
					roles={ [ Role.Patient, Role.Doctor, Role.Admin ] }
					elementsMapping={ {
						"0": <DoctorPage/>,
						"1": <DoctorPage/>,
						"2": <>home admin</>,
					} }
				/>
			},
      {
				path: "/doctors",
				element: <ProtectedRoute
					roles={ [ Role.Patient, Role.Doctor, Role.Admin ] }
					elementsMapping={ {
						"0": <DoctorsList/>,
						"1": <DoctorsList/>,
						"2": <>home admin</>,
					} }
				/>,
			},
      {
				path: "/doctors/appointment/:id",
				element: <ProtectedRoute
					roles={ [ Role.Patient ] }
					elementsMapping={ {
						"0": <AppointmentFormPage/>,
					} }
				/>,
			},
      {
				path: "/",
        element: (
          <ProtectedRoute
            roles={[Role.Patient, Role.Doctor, Role.Admin]}
            elementsMapping={{
							"0": <Home/>,
							"1": <Home/>,
							"2": <Home/>,
            }}
          />
        ),
      },
			{
				path: "/help",
				element: (
					<ProtectedRoute
						roles={ [ Role.Patient, Role.Doctor, Role.Admin ] }
						elementsMapping={ {
							"0": <Help/>,
							"1": <Help/>,
							"2": <Help/>,
						} }
					/>
				),
			},
			{
				path: "/report-bug",
				element: (
					<ProtectedRoute
						roles={ [ Role.Patient, Role.Doctor, Role.Admin ] }
						elementsMapping={ {
							"0": <BugPage/>,
							"1": <BugPage/>,
							"2": <BugPage/>,
						} }
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
