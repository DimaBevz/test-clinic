import { Navigate, useLocation } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "@store/index.ts";
import { authSelectors, authThunks } from "@store/auth";
import { Role } from "@interfaces/IAuth.ts";
import { Preloader } from "@components/preloader/Preloader";
import { NoAccessPage } from "@pages/Landing/NoAccessPage";
import { useEffect, useState } from "react";

type ElementsRoleMapping = {
  [key in Role]?: JSX.Element;
};

const ProtectedRoute: React.FC<{
  children?: JSX.Element;
  roles?: Role[];
  elementsMapping?: ElementsRoleMapping;
}> = ({ children, roles, elementsMapping }) => {
  const location = useLocation();
  const dispatch = useAppDispatch();
  const status = useAppSelector(authSelectors.getStatus);
  const user = useAppSelector(authSelectors.getAuthUser);
  const [isChecking, setIsChecking] = useState(false);
  const [isRole, setIsRole] = useState(false);

  useEffect(() => {
    !user && dispatch(authThunks.getCurrentUser());
  }, [user]);

  useEffect(() => {
    setIsChecking(true);
    if (user) {
      setIsRole(
        roles?.some((userRole: any) => user?.role === userRole) ?? true
      );
      setIsChecking(false);
    }
  }, [user, location]);

  if (!(status === "success") || isChecking || !user) {
    return <Preloader size="xl" />;
  }

  if (status === "success") {
    if (isRole) {
      if (elementsMapping) {
        return (
          <>
            {
              elementsMapping[
                user?.role as unknown as keyof ElementsRoleMapping
              ]
            }
          </>
        );
      }
      return <>{children}</>;
    } else {
      return <NoAccessPage text="У вас немає доступу до цієї сторінки" />;
    }
  } else {
    return <Navigate to="/login" replace state={{ path: location.pathname }} />;
  }
};

export default ProtectedRoute;
