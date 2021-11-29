﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TedisNetDataSimulator
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TedisNetEntities : DbContext
    {
        public TedisNetEntities()
            : base("name=TedisNetEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AlertDestination> AlertDestinations { get; set; }
        public virtual DbSet<AlertEventLevel> AlertEventLevels { get; set; }
        public virtual DbSet<AlertExecution> AlertExecutions { get; set; }
        public virtual DbSet<AlertFile> AlertFiles { get; set; }
        public virtual DbSet<AlertInterval> AlertIntervals { get; set; }
        public virtual DbSet<AlertParameterValue> AlertParameterValues { get; set; }
        public virtual DbSet<AlertPendingExecution> AlertPendingExecutions { get; set; }
        public virtual DbSet<AlertPort> AlertPorts { get; set; }
        public virtual DbSet<AlertTag> AlertTags { get; set; }
        public virtual DbSet<AlertTemporarilyDisabled> AlertTemporarilyDisableds { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Change> Changes { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelState> ChannelStates { get; set; }
        public virtual DbSet<CommandExecution> CommandExecutions { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<ControlPosition> ControlPositions { get; set; }
        public virtual DbSet<Control> Controls { get; set; }
        public virtual DbSet<CustomUserRight> CustomUserRights { get; set; }
        public virtual DbSet<DataActivation> DataActivations { get; set; }
        public virtual DbSet<DataState> DataStates { get; set; }
        public virtual DbSet<DevicePropertyValue> DevicePropertyValues { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceState> DeviceStates { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Network> Networks { get; set; }
        public virtual DbSet<NodePolyline> NodePolylines { get; set; }
        public virtual DbSet<NodePosition> NodePositions { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<NotePosition> NotePositions { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Operand> Operands { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Polyline> Polylines { get; set; }
        public virtual DbSet<Port> Ports { get; set; }
        public virtual DbSet<PortState> PortStates { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SerialPort> SerialPorts { get; set; }
        public virtual DbSet<TagIntervalSummary> TagIntervalSummaries { get; set; }
        public virtual DbSet<TagIntervalValue> TagIntervalValues { get; set; }
        public virtual DbSet<TagPropertyValue> TagPropertyValues { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagScale> TagScales { get; set; }
        public virtual DbSet<TagSet> TagSets { get; set; }
        public virtual DbSet<TagValueChanx> TagValueChanges { get; set; }
        public virtual DbSet<TagValue> TagValues { get; set; }
    }
}
