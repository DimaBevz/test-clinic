import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "@store/index.ts";
import { authSelectors, authThunks } from "@features/auth";
import { Role } from "@interfaces/IAuth.ts";
import { NoAccess } from "@pages/NoAccessPage/NoAccess.tsx";
import { Preloader } from "@components/preloader/Preloader";

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
	const [isChecking, setIsChecking] = React.useState(false);
	const [isRole, setIsRole] = React.useState(false);
	
	React.useEffect(() => {
			!user && dispatch(authThunks.getCurrentUser());
	}, [user]);
	
	React.useEffect(() => {
		setIsChecking(true);
		if (user) {
			setIsRole(
				roles?.some((userRole: any) =>
					user?.role === userRole
				) ?? true
			);
			setIsChecking(false);
		}
	},[user, location]);
	
	if (!(status === "success") || isChecking || !user) {
		return <Preloader size="xl"/>
	}
	
	if (status  === "success") {
		if (isRole) {
			if (elementsMapping) {
				return <>{elementsMapping[user?.role as unknown as keyof ElementsRoleMapping]}</>;
			}
			return <>{children}</>;
		} else {
			return <NoAccess text="У вас немає доступу до цієї сторінки" />;
		}
	} else {
		return <Navigate to="/login" replace state={{ path: location.pathname }} />;
	}
};

export default ProtectedRoute;
