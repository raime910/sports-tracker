﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StatTrack.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StatTrackEntities : DbContext
    {
        public StatTrackEntities()
            : base("name=StatTrackEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LeaguePhase> LeaguePhases { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<LeagueSet> LeagueSets { get; set; }
        public virtual DbSet<LeagueType> LeagueTypes { get; set; }
        public virtual DbSet<PlayerProfile> PlayerProfiles { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<SportType> SportTypes { get; set; }
        public virtual DbSet<TeamRole> TeamRoles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamMembership> TeamMemberships { get; set; }
        public virtual DbSet<AppClaim> AppClaims { get; set; }
        public virtual DbSet<AppLogin> AppLogins { get; set; }
        public virtual DbSet<AppRole> AppRoles { get; set; }
        public virtual DbSet<AppUserRole> AppUserRoles { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
    }
}