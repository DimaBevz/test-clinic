import capitalizeFirstLetter from './capitalize';
import createUseExternalEvents from './create-use-external-events';
import debounce from './debounce';
import { formatWorkExperience } from './formatWorkExperience';
import getCurrentMonth from './getCurrentMonth';
import isProd from './is-prod';

import randomId from './random-id';
import removeEmptyFieldsFromRequestBody from './remove-empty-fields-from-form-data';
import useGetUserBadge from './useGetUserBadge';

export * from './api';
export * from './validation';

export {
	debounce,
	isProd,
	formatWorkExperience,
	useGetUserBadge,
	createUseExternalEvents,
	getCurrentMonth,
	randomId,
	capitalizeFirstLetter,
	removeEmptyFieldsFromRequestBody,
};
