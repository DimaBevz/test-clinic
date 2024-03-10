export enum Role {
  Patient,
  Doctor ,
  Admin ,
}

export enum DisabilityCategory {
  MusculoskeletalSystemDisorders,
  IntellectualAndPsychoDisorders,
  HearingImpairment,
  VisualImpairment,
  InternalOrgansDisorders,
}

export interface IAuthData {
  id: string
  email: string
  firstName: string
  lastName: string
  patronymic: any
  phoneNumber: string
  birthday: any
  sex: string
  role: number
  photoUrl: string | null
}

export interface IAuthResponse {
  userInfo: IAuthData
}

export interface AuthStateProps {
  data: IAuthData | null;
  status: string;
}
