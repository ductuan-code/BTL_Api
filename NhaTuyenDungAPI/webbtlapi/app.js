var app = angular.module("EmployerApp", []);

app.constant("CONFIG",{
API:"https://localhost:44327/api",
MA_NGUOI_DUNG:"CFA2085F-5CD6-420D-B148-494A674014B8"
});

app.controller("MainCtrl",function($scope,$http,CONFIG){

$scope.page='companies';
$scope.setPage=p=>{$scope.page=p};

$scope.companies=[];$scope.jobs=[];$scope.applications=[];$scope.interviews=[];
$scope.companyForm={};$scope.jobForm={};$scope.interviewForm={};$scope.offerForm={};

function loadCompanies(){
$http.get(CONFIG.API+"/employer/companies/"+CONFIG.MA_NGUOI_DUNG)
.then(r=>$scope.companies=r.data);
}
function loadJobs(){
$http.get(CONFIG.API+"/employer/jobs/"+CONFIG.MA_NGUOI_DUNG)
.then(r=>$scope.jobs=r.data);
}
function loadInterviews(){
$http.get(CONFIG.API+"/employer/phong-van/employer/"+CONFIG.MA_NGUOI_DUNG)
.then(r=>$scope.interviews=r.data);
}

loadCompanies();loadJobs();loadInterviews();

/* ===== COMPANY ===== */
$scope.saveCompany=function(){
if($scope.companyForm.maCongTy)
$http.put(CONFIG.API+"/employer/companies/"+$scope.companyForm.maCongTy,$scope.companyForm)
.then(()=>{loadCompanies();$scope.companyForm={};});
else
$http.post(CONFIG.API+"/employer/companies/"+CONFIG.MA_NGUOI_DUNG,$scope.companyForm)
.then(()=>{loadCompanies();$scope.companyForm={};});
}
$scope.editCompany=c=>$scope.companyForm=angular.copy(c);
$scope.resetCompany=()=>{$scope.companyForm={};};
$scope.deleteCompany=id=>$http.delete(CONFIG.API+"/employer/companies/"+id).then(loadCompanies);

/* ===== JOB ===== */
$scope.saveJob=function(){
if($scope.jobForm.maViecLam)
$http.put(CONFIG.API+"/employer/jobs/"+$scope.jobForm.maViecLam,$scope.jobForm)
.then(()=>{loadJobs();$scope.jobForm={};});
else
$http.post(CONFIG.API+"/employer/jobs/"+CONFIG.MA_NGUOI_DUNG,$scope.jobForm)
.then(()=>{loadJobs();$scope.jobForm={};});
}
$scope.editJob=j=>$scope.jobForm=angular.copy(j);
$scope.resetJob=()=>{$scope.jobForm={};};
$scope.toggleJob=id=>$http.put(CONFIG.API+"/employer/jobs/"+id+"/toggle").then(loadJobs);
$scope.deleteJob=id=>$http.delete(CONFIG.API+"/employer/jobs/"+id).then(loadJobs);
$scope.viewJob=id=>$http.get(CONFIG.API+"/employer/jobs/detail/"+id).then(r=>$scope.jobDetail=r.data);

/* ===== APPLICATION ===== */
$scope.loadApplications=()=> $http.get(CONFIG.API+"/applications/vieclam/"+$scope.selectedJob)
.then(r=>$scope.applications=r.data);
$scope.updateDon=(id,st)=> $http.put(CONFIG.API+"/applications/"+id+"/trangthai",null,{params:{trangThai:st}})
.then($scope.loadApplications);

/* ===== INTERVIEW ===== */
$scope.saveInterview=function(){
$http.post(CONFIG.API+"/employer/phong-van/"+CONFIG.MA_NGUOI_DUNG,$scope.interviewForm)
.then(()=>{loadInterviews();$scope.interviewForm={};});
}
$scope.editInterview=p=>$scope.interviewForm=angular.copy(p);
$scope.updateInterviewStatus=(id,st)=>$http.put(CONFIG.API+"/employer/phong-van/"+id+"/trang-thai",null,{params:{trangThai:st}})
.then(loadInterviews);

/* ===== OFFER ===== */
$scope.prepareOffer=maDon=>{$scope.page='offers';$scope.offerForm.maDon=maDon;}
$scope.sendOffer=()=> $http.post(CONFIG.API+"/employer/offers/"+CONFIG.MA_NGUOI_DUNG,$scope.offerForm)
.then(()=>alert("Đã gửi offer"));
$scope.loadOffer=()=> $http.get(CONFIG.API+"/employer/offers/don/"+$scope.maDonOffer)
.then(r=>$scope.offer=r.data);
$scope.updateOfferStatus=st=> $http.put(CONFIG.API+"/employer/offers/"+$scope.offer.maOffer+"/trang-thai",null,{params:{trangThai:st}})
.then(()=>alert("Đã cập nhật"));
});
