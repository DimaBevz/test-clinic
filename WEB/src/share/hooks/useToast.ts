import { useToast as chakraToast } from '@chakra-ui/react'
export const useToast = () :{
	errorToast: ( text: string, onClose?: () => void ) => void,
	successToast: ( text: string, onClose?: () => void ) => void,
	infoToast: ( text: string ) => void,
	loadingToast: ( text: string ) => void
}=> {
	const toast = chakraToast();
	const errorToast = (text: string, onClose?: () => void) =>
		toast({
			title: text,
			onCloseComplete: onClose,
			status: "error",
			duration: 2000,
			isClosable: true,
			position: "top-right",
		})
	const successToast = (text: string, onClose?: () => void) =>
		toast({
			title: text,
			onCloseComplete: onClose,
			status: "success",
			duration: 2000,
			isClosable: true,
			position: "top-right",
		})
	const infoToast = (text: string) =>
		toast({
			title: text,
			status: "info",
			duration: 2000,
			isClosable: true,
			position: "top-right",
		})
	const loadingToast = (text: string) =>
		toast({
			title: text,
			status: "loading",
			duration: 2000,
			isClosable: true,
			position: "top-right",
		})
	return { successToast, errorToast, infoToast, loadingToast };
}

