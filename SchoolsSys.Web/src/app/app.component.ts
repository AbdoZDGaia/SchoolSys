import { AfterViewInit, Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LookupsDTO } from './models/lookups.model';
import { MapMarkersDTO } from './models/map-markers-dto';
import { StudentDTO } from './models/students.model';
import { QueryService } from './services/query.service';
import { StudentsService } from './services/students.service';
import { config } from './Shared/config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SchoolsSysAng';
  grades: LookupsDTO[];
  classes: LookupsDTO[];
  dropOffMarker: MapMarkersDTO;
  pickupMarker: MapMarkersDTO;
  imageUrl: string;
  inCallback: boolean;
  isMapEnabled: boolean;
  markerState: string;
  relativePath: string;
  uploadReset: boolean;
  uploadFilesReset: boolean;
  filesUploaded: string[] = [];
  formData: StudentDTO;
  afuConfig = {
    multiple: true,
    hideResetBtn: true,
    uploadAPI: {
      url: config.EndPoint + config.Students.UploadFilesUrl
    }
  };

  public response: { dbPath: '' };

  constructor(private queryService: QueryService,
    public studentService: StudentsService,
    private toast: ToastrService) {
  }

  ngOnInit(): void {
    this.dropOffMarker = { Latitude: 29.966674, Longitude: 31.255648, type: 'dropoff' }
    this.pickupMarker = { Latitude: 29.966822, Longitude: 31.246718, type: 'pickup' }
    this.queryService.getAllGrades().subscribe(result => {
      this.grades = result;
    }, error => {
      console.error(error);
    });

    this.imageUrl = 'assets/images/placeholder.jpg';
    this.resetForm();

    this.uploadReset = false;
    this.uploadFilesReset = false;

  }

  resetForm(form?: NgForm) {
    this.isMapEnabled = false;
    this.markerState = 'default';
    this.relativePath = null;

    if (form != null)
      form.resetForm();

    this.formData = {
      StudentId: null,
      BirthDate: null,
      ClassId: null,
      DropOffLatitude: null,
      DropOffLongitude: null,
      Email: null,
      FirstName: null,
      GenderTypeId: null,
      GradeId: null,
      LastName: null,
      MiddleName: null,
      MobileNumber: null,
      PickupLatitude: null,
      PickupLongitude: null,
      ProfilePicturePath: this.imageUrl,
      SubscribedToBus: null,
      Attachments: []
    }
    this.dropOffMarker = { Latitude: null, Longitude: null, type: 'dropoff' }
    this.pickupMarker = { Latitude: null, Longitude: null, type: 'pickup' }
    this.uploadReset = true;
    this.uploadFilesReset = true;
  }

  onSubmit(form: NgForm) {
    this.inCallback = true;
    if (form.valid)
      this.addStudent(form);
    else {
      this.toast.error(`Invalid data were entered`, 'Failed to save')
    }
    this.inCallback = false;
  }

  addStudent(form: NgForm) {
    this.formData.ProfilePicturePath = this.relativePath;
    this.studentService.addStudent(this.formData).subscribe(result => {
      this.toast.success(`Student added successfully`, 'Success')
      this.resetForm(form);
    })
  }

  public uploadFinished = (event) => {
    this.uploadReset = false;
    this.relativePath = event;
    this.formData.ProfilePicturePath = config.EndPoint + this.relativePath;
    console.log('image uploaded src:', this.formData.ProfilePicturePath);
  }

  public FileUploaded = (event) => {
    this.uploadFilesReset = false;
    this.filesUploaded = this.filesUploaded.concat(event.body);
    this.formData.Attachments = this.filesUploaded;
  }

  onSelect(id: number) {
    this.formData.ClassId = null;
    this.queryService.getClassesByGradeId(id).subscribe((result) => {
      this.classes = result.filter((item: LookupsDTO) => item.Id == id);
    });
  }

  addMarkers(markers: MapMarkersDTO[]) {
    this.formData.PickupLatitude = markers[0].Latitude;
    this.formData.PickupLongitude = markers[0].Longitude;
    this.formData.DropOffLatitude = markers[1].Latitude;
    this.formData.DropOffLongitude = markers[1].Longitude;
    this.dropOffMarker = { Latitude: this.formData.DropOffLatitude, Longitude: this.formData.DropOffLongitude, type: 'dropoff' }
    this.pickupMarker = { Latitude: this.formData.PickupLatitude, Longitude: this.formData.PickupLongitude, type: 'pickup' }
  }
}
