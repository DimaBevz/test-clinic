import { t } from "i18next";

export function formatWorkExperience(years: number) {
	if (years === 0) {
		return `${t("No experience")}`;
	} else if (years === 1) {
		return `1 ${t("year of experience")}`;
	} else {
		return `${years} ${t("years of experience")}`
	}
}
