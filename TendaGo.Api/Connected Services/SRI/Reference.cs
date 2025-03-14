﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TendaGo.Api.SRI {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    //[System.ServiceModel.ServiceContractAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", ConfigurationName="SRI.AutorizacionComprobantesOffline")]
    public interface AutorizacionComprobantesOffline {
        
        // CODEGEN: Parameter 'RespuestaAutorizacionComprobante' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        //[System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        //[System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        //[System.ServiceModel.ServiceKnownTypeAttribute(typeof(mensaje[]))]
        //[System.ServiceModel.ServiceKnownTypeAttribute(typeof(autorizacion[]))]
        //[return: System.ServiceModel.MessageParameterAttribute(Name="RespuestaAutorizacionComprobante")]
        //TendaGo.Api.SRI.autorizacionComprobanteResponse autorizacionComprobante(TendaGo.Api.SRI.autorizacionComprobante request);
        
        //[System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        //System.Threading.Tasks.Task<TendaGo.Api.SRI.autorizacionComprobanteResponse> autorizacionComprobanteAsync(TendaGo.Api.SRI.autorizacionComprobante request);
        
        //// CODEGEN: Parameter 'RespuestaAutorizacionLote' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        //[System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        //[System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        //[System.ServiceModel.ServiceKnownTypeAttribute(typeof(mensaje[]))]
        //[System.ServiceModel.ServiceKnownTypeAttribute(typeof(autorizacion[]))]
        //[return: System.ServiceModel.MessageParameterAttribute(Name="RespuestaAutorizacionLote")]
        //TendaGo.Api.SRI.autorizacionComprobanteLoteResponse autorizacionComprobanteLote(TendaGo.Api.SRI.autorizacionComprobanteLote request);
        
        //[System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        //System.Threading.Tasks.Task<TendaGo.Api.SRI.autorizacionComprobanteLoteResponse> autorizacionComprobanteLoteAsync(TendaGo.Api.SRI.autorizacionComprobanteLote request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class respuestaComprobante : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string claveAccesoConsultadaField;
        
        private string numeroComprobantesField;
        
        private autorizacion[] autorizacionesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string claveAccesoConsultada {
            get {
                return this.claveAccesoConsultadaField;
            }
            set {
                this.claveAccesoConsultadaField = value;
                this.RaisePropertyChanged("claveAccesoConsultada");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string numeroComprobantes {
            get {
                return this.numeroComprobantesField;
            }
            set {
                this.numeroComprobantesField = value;
                this.RaisePropertyChanged("numeroComprobantes");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public autorizacion[] autorizaciones {
            get {
                return this.autorizacionesField;
            }
            set {
                this.autorizacionesField = value;
                this.RaisePropertyChanged("autorizaciones");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class autorizacion : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string estadoField;
        
        private string numeroAutorizacionField;
        
        private System.DateTime fechaAutorizacionField;
        
        private bool fechaAutorizacionFieldSpecified;
        
        private string ambienteField;
        
        private string comprobanteField;
        
        private mensaje[] mensajesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
                this.RaisePropertyChanged("estado");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string numeroAutorizacion {
            get {
                return this.numeroAutorizacionField;
            }
            set {
                this.numeroAutorizacionField = value;
                this.RaisePropertyChanged("numeroAutorizacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public System.DateTime fechaAutorizacion {
            get {
                return this.fechaAutorizacionField;
            }
            set {
                this.fechaAutorizacionField = value;
                this.RaisePropertyChanged("fechaAutorizacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fechaAutorizacionSpecified {
            get {
                return this.fechaAutorizacionFieldSpecified;
            }
            set {
                this.fechaAutorizacionFieldSpecified = value;
                this.RaisePropertyChanged("fechaAutorizacionSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string ambiente {
            get {
                return this.ambienteField;
            }
            set {
                this.ambienteField = value;
                this.RaisePropertyChanged("ambiente");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string comprobante {
            get {
                return this.comprobanteField;
            }
            set {
                this.comprobanteField = value;
                this.RaisePropertyChanged("comprobante");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public mensaje[] mensajes {
            get {
                return this.mensajesField;
            }
            set {
                this.mensajesField = value;
                this.RaisePropertyChanged("mensajes");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class mensaje : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string identificadorField;
        
        private string mensaje1Field;
        
        private string informacionAdicionalField;
        
        private string tipoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string identificador {
            get {
                return this.identificadorField;
            }
            set {
                this.identificadorField = value;
                this.RaisePropertyChanged("identificador");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("mensaje", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string mensaje1 {
            get {
                return this.mensaje1Field;
            }
            set {
                this.mensaje1Field = value;
                this.RaisePropertyChanged("mensaje1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string informacionAdicional {
            get {
                return this.informacionAdicionalField;
            }
            set {
                this.informacionAdicionalField = value;
                this.RaisePropertyChanged("informacionAdicional");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
                this.RaisePropertyChanged("tipo");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4161.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ec.gob.sri.ws.autorizacion")]
    public partial class respuestaLote : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string claveAccesoLoteConsultadaField;
        
        private string numeroComprobantesLoteField;
        
        private autorizacion[] autorizacionesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string claveAccesoLoteConsultada {
            get {
                return this.claveAccesoLoteConsultadaField;
            }
            set {
                this.claveAccesoLoteConsultadaField = value;
                this.RaisePropertyChanged("claveAccesoLoteConsultada");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string numeroComprobantesLote {
            get {
                return this.numeroComprobantesLoteField;
            }
            set {
                this.numeroComprobantesLoteField = value;
                this.RaisePropertyChanged("numeroComprobantesLote");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public autorizacion[] autorizaciones {
            get {
                return this.autorizacionesField;
            }
            set {
                this.autorizacionesField = value;
                this.RaisePropertyChanged("autorizaciones");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //[System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobante", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobante {
        
        //[System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string claveAccesoComprobante;
        
        public autorizacionComprobante() {
        }
        
        public autorizacionComprobante(string claveAccesoComprobante) {
            this.claveAccesoComprobante = claveAccesoComprobante;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //[System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobanteResponse", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobanteResponse {
        
        //[System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TendaGo.Api.SRI.respuestaComprobante RespuestaAutorizacionComprobante;
        
        public autorizacionComprobanteResponse() {
        }
        
        public autorizacionComprobanteResponse(TendaGo.Api.SRI.respuestaComprobante RespuestaAutorizacionComprobante) {
            this.RespuestaAutorizacionComprobante = RespuestaAutorizacionComprobante;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //[System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobanteLote", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobanteLote {
        
        //[System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string claveAccesoLote;
        
        public autorizacionComprobanteLote() {
        }
        
        public autorizacionComprobanteLote(string claveAccesoLote) {
            this.claveAccesoLote = claveAccesoLote;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //[System.ServiceModel.MessageContractAttribute(WrapperName="autorizacionComprobanteLoteResponse", WrapperNamespace="http://ec.gob.sri.ws.autorizacion", IsWrapped=true)]
    public partial class autorizacionComprobanteLoteResponse {
        
        //[System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ec.gob.sri.ws.autorizacion", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public TendaGo.Api.SRI.respuestaLote RespuestaAutorizacionLote;
        
        public autorizacionComprobanteLoteResponse() {
        }
        
        public autorizacionComprobanteLoteResponse(TendaGo.Api.SRI.respuestaLote RespuestaAutorizacionLote) {
            this.RespuestaAutorizacionLote = RespuestaAutorizacionLote;
        }
    }
    
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    //public interface AutorizacionComprobantesOfflineChannel : TendaGo.Api.SRI.AutorizacionComprobantesOffline, System.ServiceModel.IClientChannel {
    //}
    
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    //public partial class AutorizacionComprobantesOfflineClient : System.ServiceModel.ClientBase<TendaGo.Api.SRI.AutorizacionComprobantesOffline>, TendaGo.Api.SRI.AutorizacionComprobantesOffline {
        
    //    public AutorizacionComprobantesOfflineClient() {
    //    }
        
    //    public AutorizacionComprobantesOfflineClient(string endpointConfigurationName) : 
    //            base(endpointConfigurationName) {
    //    }
        
    //    public AutorizacionComprobantesOfflineClient(string endpointConfigurationName, string remoteAddress) : 
    //            base(endpointConfigurationName, remoteAddress) {
    //    }
        
    //    public AutorizacionComprobantesOfflineClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
    //            base(endpointConfigurationName, remoteAddress) {
    //    }
        
    //    public AutorizacionComprobantesOfflineClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
    //            base(binding, remoteAddress) {
    //    }
        
    //    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //    TendaGo.Api.SRI.autorizacionComprobanteResponse TendaGo.Api.SRI.AutorizacionComprobantesOffline.autorizacionComprobante(TendaGo.Api.SRI.autorizacionComprobante request) {
    //        return base.Channel.autorizacionComprobante(request);
    //    }
        
    //    public TendaGo.Api.SRI.respuestaComprobante autorizacionComprobante(string claveAccesoComprobante) {
    //        TendaGo.Api.SRI.autorizacionComprobante inValue = new TendaGo.Api.SRI.autorizacionComprobante();
    //        inValue.claveAccesoComprobante = claveAccesoComprobante;
    //        TendaGo.Api.SRI.autorizacionComprobanteResponse retVal = ((TendaGo.Api.SRI.AutorizacionComprobantesOffline)(this)).autorizacionComprobante(inValue);
    //        return retVal.RespuestaAutorizacionComprobante;
    //    }
        
    //    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //    System.Threading.Tasks.Task<TendaGo.Api.SRI.autorizacionComprobanteResponse> TendaGo.Api.SRI.AutorizacionComprobantesOffline.autorizacionComprobanteAsync(TendaGo.Api.SRI.autorizacionComprobante request) {
    //        return base.Channel.autorizacionComprobanteAsync(request);
    //    }
        
    //    public System.Threading.Tasks.Task<TendaGo.Api.SRI.autorizacionComprobanteResponse> autorizacionComprobanteAsync(string claveAccesoComprobante) {
    //        TendaGo.Api.SRI.autorizacionComprobante inValue = new TendaGo.Api.SRI.autorizacionComprobante();
    //        inValue.claveAccesoComprobante = claveAccesoComprobante;
    //        return ((TendaGo.Api.SRI.AutorizacionComprobantesOffline)(this)).autorizacionComprobanteAsync(inValue);
    //    }
        
    //    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //    TendaGo.Api.SRI.autorizacionComprobanteLoteResponse TendaGo.Api.SRI.AutorizacionComprobantesOffline.autorizacionComprobanteLote(TendaGo.Api.SRI.autorizacionComprobanteLote request) {
    //        return base.Channel.autorizacionComprobanteLote(request);
    //    }
        
    //    public TendaGo.Api.SRI.respuestaLote autorizacionComprobanteLote(string claveAccesoLote) {
    //        TendaGo.Api.SRI.autorizacionComprobanteLote inValue = new TendaGo.Api.SRI.autorizacionComprobanteLote();
    //        inValue.claveAccesoLote = claveAccesoLote;
    //        TendaGo.Api.SRI.autorizacionComprobanteLoteResponse retVal = ((TendaGo.Api.SRI.AutorizacionComprobantesOffline)(this)).autorizacionComprobanteLote(inValue);
    //        return retVal.RespuestaAutorizacionLote;
    //    }
        
    //    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    //    System.Threading.Tasks.Task<TendaGo.Api.SRI.autorizacionComprobanteLoteResponse> TendaGo.Api.SRI.AutorizacionComprobantesOffline.autorizacionComprobanteLoteAsync(TendaGo.Api.SRI.autorizacionComprobanteLote request) {
    //        return base.Channel.autorizacionComprobanteLoteAsync(request);
    //    }
        
    //    public System.Threading.Tasks.Task<TendaGo.Api.SRI.autorizacionComprobanteLoteResponse> autorizacionComprobanteLoteAsync(string claveAccesoLote) {
    //        TendaGo.Api.SRI.autorizacionComprobanteLote inValue = new TendaGo.Api.SRI.autorizacionComprobanteLote();
    //        inValue.claveAccesoLote = claveAccesoLote;
    //        return ((TendaGo.Api.SRI.AutorizacionComprobantesOffline)(this)).autorizacionComprobanteLoteAsync(inValue);
    //    }
    //}
}
