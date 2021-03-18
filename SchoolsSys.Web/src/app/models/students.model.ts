export interface StudentDTO {
  StudentId: number;
  FirstName: string;
  MiddleName: string;
  LastName: string;
  GradeId: number;
  ClassId: number;
  BirthDate: string;
  GenderTypeId: number;
  PickupLongitude: number;
  PickupLatitude: number;
  DropOffLongitude: number;
  DropOffLatitude: number;
  SubscribedToBus: boolean;
  MobileNumber: string;
  Email: string;
  ProfilePicturePath: string;
  Attachments: string[];
}
