import { Box, Button, Heading, HStack, IconButton, Tag } from "@chakra-ui/react";
import { useRef } from "react";
import { useTranslation } from "react-i18next";
import { CloseIcon } from "@chakra-ui/icons";

import "./index.scss";
import documentsService from "@features/Documents/documents.service.ts";
import { Preloader } from "@components/preloader/Preloader";
import { useToast } from "@hooks/useToast.ts";
import { NoContent } from "@components/index.ts";

interface IDocumentsProps {
	userId: string;
	isGuest?: boolean;
}

const Documents = ( { userId, isGuest }: IDocumentsProps ) => {
	const { t } = useTranslation();
	const { successToast } = useToast();
	const hiddenFileInput = useRef<HTMLInputElement>( null );
	const { isLoading, data, refetch } = documentsService.useGetDocumentsByIdQuery( userId );
	const [ uploadDoc ] = documentsService.useUploadDocumentMutation();
	const [ deleteDoc, { isLoading: isDeleteLoading } ] = documentsService.useDeleteDocumentMutation();
	const handleClick = () => {
		hiddenFileInput.current?.click();
	};
	
	const handleChange = async ( event: any ) => {
		const formData = new FormData();
		formData.append( "file", event.target.files[0] );
		await uploadDoc( formData );
		successToast( t( "Document uploaded" ) );
	};
	
	const handleDelete = async ( id: string) => {
		await deleteDoc( id );
		await refetch();
		successToast( t( "Document deleted" ) );
	}
	
	const openDocument = ( url: string ) => {
		window.open( url, "_blank" );
	};
	
	return (
		<Box w="100%">
			<HStack justifyContent="space-between">
				<Heading as={ "h4" } size={ "md" }>{ t( "Documents" ) }</Heading>
				<Button colorScheme="orange" display={isGuest ? "none" : "block"} onClick={ handleClick } size="sm">{ t( "Upload documents" ) }</Button>
				<input
					type="file"
					onChange={ handleChange }
					accept=".doc, .docx,.ppt, .pptx,.txt,.pdf"
					ref={ hiddenFileInput }
					style={ { display: "none" } }
				/>
			</HStack>
			<HStack position="relative" minHeight="150px" maxHeight="240px" overflowY="auto" alignItems="flex-start" alignContent="flex-start" wrap="wrap"
							pt={ 4 }>
				{ !data?.length && <NoContent content={t("No documents yet")}/>	}
				{ isLoading ? <Preloader size="xl"/> : data?.map( ( file ) => (
					<Tag _hover={ { cursor: "pointer", color: "green" } } minH="40px" onClick={ () => openDocument( file.presignedUrl ) } w="100%" justifyContent="space-between">
						{ file.title }
						
						<IconButton
							display={isGuest ? "none" : "block"}
							icon={<CloseIcon color="red" />}
							aria-label="Delete"
							isLoading={ isDeleteLoading }
							className="Documents__deleteButton"
							onClick={ ( event ) => {
								event.stopPropagation();
								handleDelete( file.id );
							} }
						/>
					</Tag>
				) ) }
				
			</HStack>
		</Box>
	);
};

export default Documents;
